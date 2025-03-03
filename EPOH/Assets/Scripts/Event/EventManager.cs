using System;
using System.Collections;
using UnityEngine;

namespace Event
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager _instance;

        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EventManager>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(EventManager).Name);
                        _instance = singletonObject.AddComponent<EventManager>();
                    }
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
            }
            else if (_instance != this)
            {
                Destroy(gameObject); // 중복 생성 방지
            }
        }
        // 다음 이벤트 정보
        [Header("이벤트 실행 정보")]
        public string startEventID = "";
        public string currentEventID = "";
        public string nextEventID = "";
    
        // 이벤트 성공 여부 
        public Action OnResultEnd;
        public bool canExecute = true;

        void Start()
        {
            ExecuteEvent(startEventID);
        }
    
        // 이벤트 실행 가능 여부 검사
        public bool CheckExecutable(string eventID)
        {
            // 이벤트 데이터 내에 해당 이벤트 아이디가 있는지 체크
            if (string.IsNullOrEmpty(eventID))
            {
                Debug.LogWarning($"{eventID}는 널 값입니다.");
                return false;
            }
        
            if (!DataManager.Instance.events.ContainsKey(eventID))
            {
                Debug.LogWarning($"{eventID}는 존재하지 않는 이벤트입니다.");
                return false;
            }
        
            // 이벤트의 Condition Type 체크
            EventStructure curEvent = DataManager.Instance.events[eventID];
            if (curEvent.ConditionType == "or")
            {
                foreach (var condition in curEvent.Conditions)
                {
                    if (IsConditionMet(condition)) return true;
                }
            }
            else if (curEvent.ConditionType == "and")
            {
                foreach (var condition in curEvent.Conditions)
                {
                    if (!IsConditionMet(condition)) return false;
                }
            }
        
            return true;
        }
    
        // 조건이 만족되는지 체크하는 함수
        bool IsConditionMet(string conditionID)
        {
            if (conditionID.StartsWith("Progress"))
            {
                if (GameManager.instance.ProgressState.ToString() == conditionID)
                {
                    Debug.LogWarning($"조건{conditionID}을 만족합니다. 현재 진행도 : {GameManager.instance.ProgressState.ToString()}");
                    return true;
                }
                Debug.LogWarning($"현재 ProgressID가 올바르지 않습니다. {GameManager.instance.ProgressState.ToString()} != {conditionID}");
                return false;
            }

            return true;
        }
    
        // 이벤트 실행 함수
        public void ExecuteEvent(string eventID)
        {
            if (CheckExecutable(eventID))
            {
                EventStructure eventStructure = DataManager.Instance.events[eventID];
                currentEventID = eventID;
                Debug.Log($"{eventID} 실행 : {eventStructure.Description}");
                foreach (var result in eventStructure.Results)
                {
                    ExecuteResult(result);
                }
                
                // 결과를 다 실행하면 진행도 업데이트
                if (!string.IsNullOrEmpty(eventStructure.ProgressId))
                {
                    GameManager.instance.ProgressState = (GameManager.ProgressId)Enum.Parse(typeof(GameManager.ProgressId), eventStructure.ProgressId);
                    Debug.LogWarning($"진행도 업데이트! -> {eventStructure.ProgressId}");
                }
                
                // 다음 이벤트 아이디 확인
                if (!string.IsNullOrEmpty(eventStructure.NextEvent) && DataManager.Instance.events.ContainsKey(eventStructure.NextEvent))
                {
                    nextEventID = eventStructure.NextEvent;
                    Debug.LogWarning($"다음 이벤트 아이디 갱신! -> {nextEventID}");
                
                    if (DataManager.Instance.events[eventStructure.NextEvent].IsAuto == "true")
                    {
                        Debug.LogWarning($"{nextEventID}의 IsAuto 값이 true여서 바로 실행");
                        ExecuteEvent(eventStructure.NextEvent);
                    }
                }
            }
        }
    
        // 결과 실행 함수
        void ExecuteResult(string resultID)
        {
            if (string.IsNullOrEmpty(resultID)) return;

            if (resultID.StartsWith("Effect"))
            {
                StartCoroutine(DoEffect(resultID));
            }
            else if (resultID.StartsWith("Dialogue"))
            {
                DoDialogue(resultID);
            }
        }

        IEnumerator DoEffect(string effectID)
        {
            if(string.IsNullOrEmpty(effectID)) yield return null;
            if (DataManager.Instance.effects.ContainsKey(effectID))
            {
                Debug.Log($"{effectID} 이펙트 실행");
                EffectStructure effect = DataManager.Instance.effects[effectID];
                if (effect.IsWait == "true")
                {
                    yield return new WaitUntil(() => canExecute);
                }
            
                switch (effect.EffectType)
                {
                    case "ArtResource":
                        Debug.Log($"ArtResource 타입의 {effect.EffectId} 실행");
                        break;
                    case "Camera":
                        Debug.Log($"Camera 타입의 {effect.EffectId} 실행");
                        break;
                    case "Animation":
                        Debug.Log($"Animation 타입의 {effect.EffectId} 실행");
                        break;
                    case "Screen":
                        Debug.Log($"Screen 타입의 {effect.EffectId} 실행");
                        break;
                }
                Debug.Log($"{effect.Description}");
            }
        }

        void DoDialogue(string dialogueID)
        {
            if(string.IsNullOrEmpty(dialogueID)) return;
            if (DataManager.Instance.dialogues.ContainsKey(dialogueID))
            {
                canExecute = false;
                Debug.Log($"{dialogueID} 대화 실행");
                StartCoroutine(DialogueManager.Instance.StartDialogue(dialogueID));
            }
        }
    }
}

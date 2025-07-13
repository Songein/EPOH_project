using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
        public bool canExecute = true;
        
        // 카메라 관련 변수
        private int _maxPriority = 20;
        private int _minPriority = 1;

        void Start()
        {
            ExecuteEvent(startEventID).Forget();
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
        
            if (!DataManager.Instance.Events.ContainsKey(eventID))
            {
                Debug.LogWarning($"{eventID}는 존재하지 않는 이벤트입니다.");
                return false;
            }
        
            // 이벤트의 Condition Type 체크
            EventStructure curEvent = DataManager.Instance.Events[eventID];
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
            
            if (conditionID.StartsWith("Before_Progress"))
            {
                string parseProgressState = conditionID.Substring(7);
                if (Enum.TryParse(parseProgressState, false, out GameManager.ProgressId progressId))
                {
                    if (GameManager.instance.ProgressState < progressId)
                    {
                        Debug.LogWarning($"조건{conditionID}을 만족합니다. 현재 상태{GameManager.instance.ProgressState.ToString()} < {progressId.ToString()}");
                        return true;
                    }
                    Debug.LogWarning($"조건{conditionID}을 만족하지 않습니다. 현재 상태{GameManager.instance.ProgressState.ToString()} >= {progressId.ToString()}");
                    return false;
                }
                Debug.LogError($"{parseProgressState}라는 진행도 상태가 존재하지 않음.");
                return false;
            }

            if (conditionID.StartsWith("After_Progress"))
            {
                string parseProgressState = conditionID.Substring(6);
                if (Enum.TryParse(parseProgressState, false, out GameManager.ProgressId progressId))
                {
                    if (GameManager.instance.ProgressState >= progressId)
                    {
                        Debug.LogWarning($"조건{conditionID}을 만족합니다. 현재 상태{GameManager.instance.ProgressState.ToString()} >= {progressId.ToString()}");
                        return true;
                    }
                    Debug.LogWarning($"조건{conditionID}을 만족하지 않습니다. 현재 상태{GameManager.instance.ProgressState.ToString()} < {progressId.ToString()}");
                    return false;
                }
                Debug.LogError($"{parseProgressState}라는 진행도 상태가 존재하지 않음.");
                return false;
            }

            return true;
        }
    
        // 이벤트 실행 함수
        public async UniTask ExecuteEvent(string eventID)
        {
            if (CheckExecutable(eventID))
            {
                EventStructure eventStructure = DataManager.Instance.Events[eventID];
                currentEventID = eventID;
                Debug.Log($"{eventID} 실행 : {eventStructure.Description}");
                
                foreach (var result in eventStructure.Results)
                {
                    await ExecuteResult(result);
                }
                
                // 결과를 다 실행하면 진행도 업데이트
                if (!string.IsNullOrEmpty(eventStructure.ProgressId))
                {
                    GameManager.instance.ProgressState = (GameManager.ProgressId)Enum.Parse(typeof(GameManager.ProgressId), eventStructure.ProgressId);
                    Debug.LogWarning($"진행도 업데이트! -> {eventStructure.ProgressId}");
                SaveManager.instance.GettheId(eventStructure.ProgressId);
                }
                
                // 다음 이벤트 아이디 확인
                if (!string.IsNullOrEmpty(eventStructure.NextEvent) && DataManager.Instance.Events.ContainsKey(eventStructure.NextEvent))
                {
                    nextEventID = eventStructure.NextEvent;
                    Debug.LogWarning($"다음 이벤트 아이디 갱신! -> {nextEventID}");
                SaveManager.instance.GettheEvent(nextEventID);
                
                    if (DataManager.Instance.Events[eventStructure.NextEvent].IsAuto == "true")
                    {
                        Debug.LogWarning($"{nextEventID}의 IsAuto 값이 true여서 바로 실행");
                        ExecuteEvent(eventStructure.NextEvent);
                    }
                }
            }
        }
    
        // 결과 실행 함수
        private async UniTask ExecuteResult(string resultID)
        {
            if (string.IsNullOrEmpty(resultID))
            {
                return;
            }

            if (resultID.StartsWith("Effect"))
            {
                await DoEffect(resultID);
            }
            else if (resultID.StartsWith("Dialogue"))
            {
                await DoDialogue(resultID);
            }
        }

        private async UniTask DoEffect(string effectID)
        {
            if(string.IsNullOrEmpty(effectID)) UniTask.Yield();
            if (DataManager.Instance.Effects.ContainsKey(effectID))
            {
                EffectStructure effect = DataManager.Instance.Effects[effectID];
                Debug.LogWarning($"{effect.Description} 실행");
                
                switch (effect.EffectType)
                {
                    case "Request":
                        Debug.LogWarning($"Request 타입의 {effect.EffectId} 실행");
                        UIManager.Instance.OpenUI(UIManager.Instance.requestUI,effect);
                        break;
                    case "ArtResource":
                        Debug.LogWarning($"ArtResource 타입의 {effect.EffectId} 실행");
                        UIManager.Instance.OpenUI(UIManager.Instance.popUpUI,effect);
                        break;
                    case "TakeObject":
                        Debug.LogWarning($"TakeObject 타입의 {effect.EffectId} 실행");
                        UIManager.Instance.OpenUI(UIManager.Instance.takeObjectUI,effect);
                        break;
                    case "Camera":
                        Debug.LogWarning($"Camera 타입의 {effect.EffectId} 실행");
                        GameObject effectObj = GameObject.Find(effect.EffectId);
                        GameObject camera = effectObj.transform.GetChild(0).gameObject;
                        camera.SetActive(true);
                        camera.GetComponent<CinemachineVirtualCamera>().Priority = _maxPriority;
                        break;
                    case "Animation":
                        Debug.LogWarning($"Animation 타입의 {effect.EffectId} 실행");
                        break;
                    case "Screen":
                        Debug.LogWarning($"Screen 타입의 {effect.EffectId} 실행");
                        if (effect.EffectId == "Effect_011")
                        {
                            switch (GameManager.instance.ProgressState)
                            {
                                case GameManager.ProgressId.Progress_Req1_Clear:
                                    SceneChanger.Instance.ChangeScene("CutScene1").Forget();
                                    break;
                                case GameManager.ProgressId.Progress_Req2_Clear:
                                    SceneChanger.Instance.ChangeScene("CutScene2").Forget();
                                    break;
                                case GameManager.ProgressId.Progress_Req3_Clear:
                                    SceneChanger.Instance.ChangeScene("CutScene3").Forget();
                                    break;
                                case GameManager.ProgressId.Progress_Req4_Clear:
                                    SceneChanger.Instance.ChangeScene("CutScene4").Forget();
                                    break;
                            }
                        }
                        else if(effect.EffectId == "Effect_012")
                        {
                            SceneChanger.Instance.ChangeScene("MainRoomTest").Forget();
                        }
                        break;
                }
                
                if (effect.IsWait == "true")
                {
                    await WaitForEffectEndAsync();
                    Debug.LogWarning($"IsWait True 타입의 {effect.EffectId} 끝날 때까지 대기 완료");
                }
                Debug.LogWarning($"IsWait False 타입의 {effect.EffectId} 실행 끝");
            }
        }

        private async UniTask DoDialogue(string dialogueID)
        {
            if(string.IsNullOrEmpty(dialogueID)) return;
            if (DataManager.Instance.Dialogues.ContainsKey(dialogueID))
            {
                canExecute = false;
                Debug.LogWarning($"{dialogueID} 대화 실행");
                StartCoroutine(DialogueManager.Instance.StartDialogue(dialogueID));
                await WaitForDialogueEndAsync();
                Debug.LogWarning($"{dialogueID} 대화 끝");
            }
        }
        
        private async UniTask WaitForDialogueEndAsync()
        {
            DialogueManager.Instance.OnDialogueEnd = null;
            var tcs = new UniTaskCompletionSource();
            DialogueManager.Instance.OnDialogueEnd += () => tcs.TrySetResult();
            await tcs.Task;
        }
    
        private async UniTask WaitForEffectEndAsync()
        {
            EffectManager.Instance.OnEffectEnd = null;
            var tcs = new UniTaskCompletionSource();
            EffectManager.Instance.OnEffectEnd += () => tcs.TrySetResult();
            await tcs.Task;
        }
    }

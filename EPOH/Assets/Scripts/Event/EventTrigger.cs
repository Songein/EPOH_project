using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event
{
    public class EventTrigger : MonoBehaviour
    {
        [System.Serializable]
        public class EventInfo
        {
            public string eventID;
            public bool isExecuted;
        }
        [SerializeField] private List<EventInfo> _eventIDList = new List<EventInfo>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && PlayerInteract.Instance.canInteract)
            {
                foreach (var _event in _eventIDList)
                {
                    if (EventManager.Instance.CheckExecutable(_event.eventID))
                    {
                        if(_event.eventID == "Event_010" && _event.isExecuted) return;
                        switch (tag)
                        {
                            case "AutoTrigger":
                                EventManager.Instance.ExecuteEvent(_event.eventID).Forget();
                                _event.isExecuted = true;
                                return;
                            case "Interaction":
                                PlayerInteract.Instance.OnInteract += () =>
                                {
                                    Debug.LogWarning($"{_event.eventID} 플레이어 OnInteract에 할당");
                                    EventManager.Instance.ExecuteEvent(_event.eventID).Forget();
                                    _event.isExecuted = true;
                                };
                                return;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"{_event.eventID}는 현재 실행 불가능");
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.GetComponent<PlayerInteract>().canInteract)
            {
                PlayerInteract.Instance.OnInteract = null;
            }
        }
    }
}

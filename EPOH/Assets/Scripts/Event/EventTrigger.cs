using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private List<string> _eventIDList = new List<string>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && PlayerInteract.instance.canInteract)
            {
                foreach (var eventID in _eventIDList)
                {
                    if (EventManager.Instance.CheckExecutable(eventID))
                    {
                        switch (tag)
                        {
                            case "AutoTrigger":
                                EventManager.Instance.ExecuteEvent(eventID);
                                break;
                            case "Interaction":
                                PlayerInteract.instance.OnInteract += () =>
                                {
                                    Debug.LogWarning($"{eventID} 플레이어 OnInteract에 할당");
                                    EventManager.Instance.ExecuteEvent(eventID);
                                };
                                break;
                            case "Portal":
                                break;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"{eventID}는 현재 실행 불가능");
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.GetComponent<PlayerInteract>().canInteract)
            {
                PlayerInteract.instance.OnInteract = null;
            }
        }
    }
}

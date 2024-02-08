using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEvent : MonoBehaviour
{
    TalkAction action;
    private void Start()
    {
        action = FindObjectOfType<TalkAction>();
        if(GameManager.instance.story_info > 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            action.Action();
            this.gameObject.SetActive(false);
        }
    }
}

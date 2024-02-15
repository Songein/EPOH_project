using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTalk : MonoBehaviour
{
    TalkAction action;

    private void Start()
    {
        action = GameObject.FindGameObjectWithTag("TalkManager").GetComponent<TalkAction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(EventStart());
        }  
            
    }

    IEnumerator EventStart()
    {
        yield return new WaitForSeconds(0.5f);
        action.Action();
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
    
}

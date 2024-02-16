using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventArea : MonoBehaviour
{
    TalkAction action;

    private void Start()
    {
        action = FindObjectOfType<TalkAction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.instance.story_info == 5)
        {
            StartCoroutine(EventStart());
        }
    }

    IEnumerator EventStart()
    {
        action.Action();
        yield return (new WaitForSeconds(0.5f));
        this.gameObject.SetActive(false);
    }
}

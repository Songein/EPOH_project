using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2Talk : MonoBehaviour
{
    TalkAction action;
    [SerializeField] bool first_talk_end = false; // 첫번째 대화 끝 확인

    public PlayerController player_controller; 

    private void Start()
    {
        action = FindObjectOfType<TalkAction>();
    }

    private void Update()
    {
        if (GameManager.instance.story_info == 2 && !first_talk_end)
        {
            first_talk_end = true;
            action.Action();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            action.Action();
        }
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2Talk : MonoBehaviour
{
    TalkAction action;
    bool first_talk_end = false; // 첫번째 대화 끝 확인

    private PlayerController player_controller; 

    private void Start()
    {
        action = FindObjectOfType<TalkAction>();
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (GameManager.instance.story_info == 2 && !first_talk_end)
        {
            first_talk_end = true;
            action.Action();
        }
    }    
}

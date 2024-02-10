using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventArea2 : MonoBehaviour
{
    TalkAction action;
    TalkManager talkManager;

    private void Start()
    {
        action = FindObjectOfType<TalkAction>();
        talkManager = GameObject.FindGameObjectWithTag("TalkManager").GetComponent<TalkManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collider가 "Player" 태그를 가진 객체와 접촉했는지 확인
        if (collision.CompareTag("Player"))
        {
            // 조건이 충족되면 StartTalks 코루틴을 시작
            StartCoroutine(StartTalks());
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator StartTalks()
    {
        // talk_data[1] 출력
        string talk_line = talkManager.GetTalk(1, action.talk_index);
        while (talk_line != null)
        {
            action.talk_effect.SetMessage(talk_line, action.is_notice);
            action.is_talking = true;
            yield return new WaitUntil(() => action.is_talking == false);
            action.talk_index++;
            talk_line = talkManager.GetTalk(1, action.talk_index);
        }
        
        action.talk_index = 0; // talk_index 초기화

        // talk_data[2] 출력
        talk_line = talkManager.GetTalk(2, action.talk_index);
        while (talk_line != null)
        {
            action.talk_effect.SetMessage(talk_line, action.is_notice);
            action.is_talking = true;
            yield return new WaitUntil(() => action.is_talking == false);
            action.talk_index++;
            talk_line = talkManager.GetTalk(2, action.talk_index);
        }

        action.talk_index = 0; // talk_index 초기화
    }

}

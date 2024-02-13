using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event1Tutorial : MonoBehaviour
{
    private TalkAction action;
    private PlayerController player_controller;
    public GameObject tutorial_panel;

    private bool space_pressed = false;

    
    private void Start()
    {
        tutorial_panel.SetActive(false);
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        action = GameObject.FindGameObjectWithTag("TalkManager").GetComponent<TalkAction>();
    }

    private void Update()
    {
        // GameManager의 story_info가 1이고 (스토리 1이 진행되었고) tutorial_info가 0이면 tutorial_panel 활성화
        if (GameManager.instance.story_info == 1 && GameManager.instance.tutorial_info == 0)
        {
            StartCoroutine(showTutorial());
            GameManager.instance.tutorial_info++; // tutorial 카운트를 올림
        }

        if (Input.GetKeyDown(KeyCode.Space) && tutorial_panel.activeSelf) // 스페이스 키를 눌렀는지 체크
        {
            space_pressed = true;
        }
    }

    IEnumerator showTutorial()
    {
        tutorial_panel.SetActive(true); 
        player_controller.is_interacting = true; 

        yield return StartCoroutine(waitForKeyPress());
        tutorial_panel.SetActive(false); 
        player_controller.is_interacting = false;
    }

    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사로 넘어가는 함수
    {
        while (!space_pressed)
        {
            yield return null;
        }
        space_pressed = false; // Space 키를 눌렀다는 체크를 초기화
    }

}

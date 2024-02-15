using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event2Tutorial : MonoBehaviour
{
    private TalkAction action;
    private PlayerController player_controller; 

    public GameObject tutorial_panel;

    private bool space_pressed = false;
    
    private void Start()
    {
        tutorial_panel.SetActive(false);
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        action = FindObjectOfType<TalkAction>();
    }

    void Update()
    {
        // GameManager의 story_info가 3이고 tutorial_info가 1이면 tutorial_panel 활성화
        if (GameManager.instance.story_info == 3 && GameManager.instance.tutorial_info == 1)
        {
            StartCoroutine(showTutorial());
            GameManager.instance.tutorial_info++; // tutorial 카운트를 올림
        }

        if (Input.GetKeyDown(KeyCode.Space) && tutorial_panel.activeSelf) // 스페이스 키를 눌렀는지 체크
        {
            space_pressed = true;
        }
        else
        {
            space_pressed = false;  // 스페이스 키를 눌렀다는 체크를 초기화
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

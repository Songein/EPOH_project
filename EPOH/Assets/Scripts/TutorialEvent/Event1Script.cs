using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event1Script : MonoBehaviour
{
    public TalkAction action;
    public PlayerController player_controller; 

    public GameObject tutorial_panel; 
    public TMP_Text tutorial_text;

    private bool isTutorialShown = false;



    private bool space_pressed = false;
    
    private void Start()
    {
        tutorial_panel.SetActive(false);
        action = GameObject.FindGameObjectWithTag("TalkManager").GetComponent<TalkAction>();
    }

    private void Update()
    {
        // GameManager.instance.story_info가 1이면 tutorial_panel 활성화
        if (GameManager.instance.story_info == 1 && !isTutorialShown)
        {
            if(tutorial_panel.activeSelf == false && action.is_talking == false)
            {
               StartCoroutine(showTutorial());
            }

            
            if (Input.GetKeyDown(KeyCode.Space) && tutorial_panel.activeSelf) // 스페이스 키를 눌렀는지 체크
            {
                space_pressed = true;

            }
            
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

        tutorial_text.text = "튜토리얼- ";
        yield return new WaitForSeconds(0.6f); 
        StartCoroutine(printTutorialTextEffect("space로 상호작용이 가능합니다.", 0.05f));
        yield return StartCoroutine(waitForKeyPress());
        tutorial_panel.SetActive(false); 
        player_controller.is_interacting = false;

        isTutorialShown = true; // 튜토리얼이 보여졌음을 표시
    }

    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사로 넘어가는 함수
    {
        while (!space_pressed)
        {
            yield return null;
        }
        space_pressed = false; // Space 키를 눌렀다는 체크를 초기화
    }

    IEnumerator printTutorialTextEffect(string text, float delay)
    {
        for (int i = 0; i < text.Length; i++)
        {
            tutorial_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

}

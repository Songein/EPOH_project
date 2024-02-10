using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialTalk : MonoBehaviour
{
    public GameObject tutorial_panel; // 튜토리얼 Panel GameObject
    public TMP_Text tutorial_text; // 튜토리얼 TMPro Text 요소를 저장할 변수

    private TalkAction talkAction; // TalkAction 객체

    public PlayerController player_controller;

    private bool space_pressed = false; // Space 키를 눌렀는지 체크하는 변수 추가

    void Start()
    {
        talkAction = GetComponent<TalkAction>(); // TalkAction 객체 가져오기

        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        tutorial_panel.SetActive(false); //튜토리얼 Panel 비활성화
    }

   void Update()
    {
        // talkAction.talk_index 값을 로그로 출력
        Debug.Log("Talk Index: " + talkAction.talk_index);

        // Space 키 입력을 감지하고 space_pressed 값을 true로 설정
        if (Input.GetKeyDown(KeyCode.Space))
        {
            space_pressed = true;
        }
        
        if (!talkAction.is_talking) // 대화가 끝났을 경우
        {
            switch (talkAction.talk_index)
            {
                case 1: // Event 1 대화가 끝났을 경우
                    StartCoroutine(showTutorial("좌우 방향키로 이동할 수 있습니다. Z키로 점프할 수 있습니다. 점프는 최대 2번까지 연속으로 가능합니다."));
                    break;
                case 2: // Event 2 대화가 끝났을 경우
                    StartCoroutine(showTutorial("space로 상호작용이 가능합니다."));
                    break;
            }
        }
    }


    IEnumerator showTutorial(string tutorialMessage)
    {
        tutorial_panel.SetActive(true); // 튜토리얼 Panel 활성화
        player_controller.is_talking = true;
        tutorial_text.text = "튜토리얼 - ";
        yield return new WaitForSeconds(0.6f); 
        yield return StartCoroutine(printTutorialTextEffect(tutorialMessage, 0.05f)); 
        yield return waitForKeyPress();
        tutorial_panel.SetActive(false); 
        player_controller.is_talking = false;

        
    }

    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사로 넘어가는 함수
    {
        while (!space_pressed)
        {
            yield return null;
        }

        space_pressed = false; 
    }

    IEnumerator printTutorialTextEffect(string text, float delay) // 타이핑 효과
    {
        for (int i = 0; i < text.Length; i++)
        {
            tutorial_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }


}

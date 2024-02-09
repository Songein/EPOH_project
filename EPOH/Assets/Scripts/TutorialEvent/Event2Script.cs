using System.Collections;
using TMPro;
using UnityEngine;

public class Event2Script : MonoBehaviour
{
    public GameObject character_panel; // 주인공 대화창 Panel을 나타내는 게임 오브젝트
    public TMP_Text character_text; // 주인공 Dialogue TMPro Text 요소를 저장할 변수
    //private float delay_between_lines = 2f; // 대사 사이의 딜레이

    public GameObject tutorial_panel; //  튜토리얼 Panel을 저장할 변수
    public TMP_Text tutorial_text; // 튜토리얼 TMPro Text 요소를 저장할 변수
    //private float tutorial_delay = 4f;

    public GameObject event_panel; //  이벤트 Panel을 저장할 변수
    public TMP_Text event_text; // 이벤트 TMPro Text 요소를 저장할 변수
    //private float event_delay = 2f;

    private bool space_pressed = false; // Space 키를 눌렀는지 체크하는 변수 추가

    public PlayerController player_controller;



     void Start()
    {
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        character_panel.SetActive(false); //주인공 대화창 Panel 비활성화
        tutorial_panel.SetActive(false); //튜토리얼 Panel 비활성화
        event_panel.SetActive(false); //이벤트 Panel 비활성화
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            space_pressed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") // 오브젝트의 이름이 "Player"인 경우
        {
            Debug.Log("특정 범위에 들어왔습니다.");
            Debug.Log("어디선가 들려오는 소리");

            space_pressed = false; // space_pressed 값을 초기화
            StartCoroutine(startEventSound()); // 안내음 이벤트 시작
            player_controller.is_talking = true;
        }
    }

    
    IEnumerator startEventSound() 
    {
        // 이벤트 Panel 활성화
        event_panel.SetActive(true);

        //첫 번째 이벤트 안내음
        event_text.text = "안내음: ";
        yield return new WaitForSeconds(0.6f); // 약간의 딜레이 추가
        yield return StartCoroutine(printEventTextEffect("의식 접속 확인", 0.05f)); // 텍스트 효과 적용
        yield return new WaitWhile(() => !space_pressed);
        space_pressed = false;

        // 안내음 사라짐
        event_text.text = "";

        // 두 번째 안내음 표시
        event_text.text = "안내음: ";
        yield return new WaitForSeconds(0.6f); // 안내음 간의 짧은 딜레이
        yield return StartCoroutine(printEventTextEffect("직원코드 H-00. 의뢰가 할당되었습니다. 업무에 임해주시기 바랍니다.", 0.05f)); // 텍스트 효과 적용
        yield return waitForKeyPress();

        // 안내음 사라짐
        event_text.text = "";

        // 이벤트 Panel 비활성화
        event_panel.SetActive(false);
        player_controller.is_talking = false;

        // 캐릭터 대화창 Panel 활성화
        character_panel.SetActive(true);
        player_controller.is_talking = true;
        Debug.Log("캐릭터 대화창 panel 활성화");

        yield return null; // 한 프레임 대기

        if (character_panel.activeSelf) 
        {
            StartCoroutine(startCharacterDialogue()); // 캐릭터 대화 이벤트 발생
        }


    }

    IEnumerator startCharacterDialogue()
    {
        character_text.text = "나: ";
        yield return new WaitForSeconds(0.6f); // 약간의 딜레이 추가
        yield return StartCoroutine(printCharacterTextEffect("의뢰? 업무? 저쪽에 빛나고 있는 것을 조작하면 되는 건가?", 0.05f)); // 텍스트 효과 적용
        yield return waitForKeyPress();
        
        // 대사 사라짐
        character_text.text = "";

        //주인공 대화창 Panel 비활성화
        character_panel.SetActive(false);
        player_controller.is_talking = false;

        // 튜토리얼 Panel 활성화
        tutorial_panel.SetActive(true);
        player_controller.is_talking = true;
        Debug.Log("튜토리얼 panel 활성화");

        yield return null; // 한 프레임 대기

        if (tutorial_panel.activeSelf) 
        {
            StartCoroutine(tutorialEvent()); // 튜토리얼 이벤트 발생
        }

    }

    IEnumerator tutorialEvent()
    {
        tutorial_text.text = "튜토리얼- ";
         yield return new WaitForSeconds(0.6f); // 약간의 딜레이 추가
        yield return StartCoroutine(printTutorialTextEffect("space로 상호작용이 가능합니다.", 0.05f)); // 텍스트 효과 적용
        yield return waitForKeyPress();
        tutorial_panel.SetActive(false); // 튜토리얼 Panel 비활성화
        player_controller.is_talking = false;
        Debug.Log("튜토리얼 panel 비활성화");

    }
    
    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사로 넘어가는 함수
    {
        while (true)
        {
            if (space_pressed)
            {
                space_pressed = false; // Space 키를 눌렀다는 체크를 초기화
                yield break; // 루프를 빠져나와 다음 대사로 넘어감
            }

            yield return null;
        }
    }

    IEnumerator printCharacterTextEffect(string text, float delay)
    {
        for (int i = 0; i < text.Length; i++)
        {
            character_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator printTutorialTextEffect(string text, float delay)
    {
        for (int i = 0; i < text.Length; i++)
        {
            tutorial_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator printEventTextEffect(string text, float delay)
    {
        for (int i = 0; i < text.Length; i++)
        {
            event_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

}

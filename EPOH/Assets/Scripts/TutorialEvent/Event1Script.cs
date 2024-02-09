using System.Collections;
using TMPro;
using UnityEngine;

public class Event1Script : MonoBehaviour
{
    public GameObject character_panel; // 주인공 대화창 Panel을 저장할 변수
    public TMP_Text dialogue_text; // 주인공 Dialogue TMPro Text 요소를 저장할 변수
    private float delay_between_lines = 2f; // 대사 사이의 딜레이

    public GameObject tutorial_panel; //  튜토리얼 Panel을 저장할 변수
    public TMP_Text tutorial_text; // 튜토리얼 TMPro Text 요소를 저장할 변수
    private float tutorial_delay = 4f;

    void Start()
    {
        
        StartCoroutine(startCharacterEvent());

    }

    IEnumerator startCharacterEvent()
    {

        character_panel.SetActive(true); // 주인공 대화창 Panel 활성화
        tutorial_panel.SetActive(false); // 튜토리얼 Panel 비활성화

        //메인룸에서 눈을 뜨는 주인공
        Debug.Log("눈을 뜨는 주인공 애니메이션");

        // 첫 번째 대사 표시
        dialogue_text.text = "나: 여긴…?";
        yield return new WaitForSeconds(delay_between_lines);

        // 대사 사라짐
        dialogue_text.text = "";

        //주인공 걷는 모션 -> 비틀거리는 애니메이션
        Debug.Log("걷다가 비틀거리는 주인공 애니메이션");

        // 두 번째 대사 표시
        yield return new WaitForSeconds(0.5f); // 대사 간의 짧은 딜레이
        dialogue_text.text = "나: 아무 것도 기억나지 않아…";
        yield return new WaitForSeconds(delay_between_lines);

        // 대사 사라짐
        dialogue_text.text = "";

        // 주인공 대화창 Panel 비활성화
        character_panel.SetActive(false);


        // 튜토리얼 Panel 활성화
        tutorial_panel.SetActive(true);
        Debug.Log("튜토리얼 panel 활성화");

        yield return null; // 한 프레임 대기

        if (tutorial_panel.activeSelf) 
        {
            StartCoroutine(tutorialTextPrint()); // 튜토리얼 이벤트 발생
        }

    }

    IEnumerator tutorialTextPrint()
    {
        tutorial_text.text = "튜토리얼 - 좌우 방향키로 이동할 수 있습니다. Z키로 점프할 수 있습니다. 점프는 최대 2번까지 연속으로 가능합니다.";
        yield return new WaitForSeconds(tutorial_delay);
        tutorial_panel.SetActive(false); // 튜토리얼 Panel 비활성화
        Debug.Log("튜토리얼 panel 비활성화");
    }

}

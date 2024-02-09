using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Event4Script : MonoBehaviour
{
    public ScrollRect scrollRect;

    public GameObject event_panel; //  이벤트 Panel을 저장할 변수
    public TMP_Text event_text; // 이벤트 TMPro Text 요소를 저장할 변수
    //private float event_delay = 2f;

    public Button accept_button;

     private bool space_pressed = false; // Space 키를 눌렀는지 체크하는 변수 추가

    void Start()
    {
        event_text.text = ""; // event_text를 빈 문자열로 시작
        event_panel.SetActive(false);
        accept_button.onClick.AddListener(closeScrollViewAndOpenEventPanel);
        space_pressed = false; // 스크립트가 시작될 때 space_pressed를 false로 설정
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            space_pressed = true;
        }
    }

    public void closeScrollViewAndOpenEventPanel()
    {
        scrollRect.gameObject.SetActive(false); // ScrollRect 비활성화
        StartCoroutine(startEvent4Talk());
    }

    IEnumerator startEvent4Talk()
    {
        event_panel.SetActive(true); // 이벤트 Panel 활성화
        yield return new WaitForSeconds(0.1f); // 약간의 딜레이 추가

        //첫 번째 이벤트 안내음
        event_text.text = "안내음: ";
        yield return new WaitForSeconds(0.6f); // 안내음 간의 짧은 딜레이
        yield return StartCoroutine(printEventTextEffect("의뢰가 수리되었습니다. 의뢰인의 기억과 연결합니다.", 0.05f)); // 텍스트 효과 적용
        space_pressed = false; // space_pressed 초기화
        yield return new WaitUntil(() => space_pressed); // space_pressed가 참이 될 때까지 기다림

        space_pressed = false;

        // 안내음 사라짐
        event_text.text = "";

        // 두 번째 안내음 표시
        event_text.text = "안내음: ";
        yield return new WaitForSeconds(0.6f); // 안내음 간의 짧은 딜레이
        yield return StartCoroutine(printEventTextEffect("……", 0.05f)); // 텍스트 효과 적용
        yield return waitForKeyPress();


        // 안내음 사라짐
        event_text.text = "";

        // 세 번째 안내음 표시
        //yield return new WaitForSeconds(0.5f); // 안내음 간의 짧은 딜레이
        event_text.text = "안내음: ";
        yield return new WaitForSeconds(0.6f); // 안내음 간의 짧은 딜레이
        yield return StartCoroutine(printEventTextEffect("연결 완료. 전사 장치를 통해 기억에 입장해주시기 바랍니다.", 0.05f)); // 텍스트 효과 적용
        yield return waitForKeyPress();


        // 안내음 사라짐
        event_text.text = "";

        // 이벤트 Panel 비활성화
        event_panel.SetActive(false);
        
         // 대화가 끝나고 space_pressed 초기화
        space_pressed = false;

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

    IEnumerator printEventTextEffect(string text, float delay)
    {
        for (int i = 0; i < text.Length; i++)
        {
            event_text.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScrollViewControl : MonoBehaviour
{
    public ScrollRect myScrollRect; // Scroll View의 Scroll Rect를 연결
    public float scrollSpeed = 0.2f; // 스크롤 속도 조절

    public GameObject character_panel; // 주인공 대화창 Panel을 저장할 변수
    public TMP_Text dialogue_text; // 주인공 Dialogue TMPro Text 요소를 저장할 변수
    private float delay_between_lines = 2f; // 대사 사이의 딜레이
    private bool has_started_talking = false; // 대화가 시작되었는지 체크하는 변수

    /*
    public GameObject event_panel; //  이벤트 Panel을 저장할 변수
    public TMP_Text event_text; // 이벤트 TMPro Text 요소를 저장할 변수
    private float event_delay = 2f;
    */

    public Button accept_button; // Accept 버튼

    //private bool should_start_event = false; // 코루틴 시작 플래그
    


    void Start()
    {
        character_panel.SetActive(false); // 주인공 대화창 Panel 비활성화
        //event_panel.SetActive(false); //이벤트 Panel 비활성화

        accept_button.gameObject.SetActive(false); // Accept 버튼 비활성화

        /*
        accept_button.onClick.AddListener(() => {
            myScrollRect.gameObject.SetActive(false); // ScrollRect 비활성화
            //StartCoroutine(startEvent4Talk()); // 이벤트 안내음 
        };
        */

    }
    

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) // 위 방향키를 누르면
        {
            // Scroll View의 위치를 위로 이동시킵니다.
            myScrollRect.verticalNormalizedPosition += scrollSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow)) // 아래 방향키를 누르면
        {
            // Scroll View의 위치를 아래로 이동시킵니다.
            myScrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !has_started_talking) // Space 키를 누르면
        {
            // Scroll View의 위치를 맨 끝으로 이동시킵니다.
            myScrollRect.verticalNormalizedPosition = 0;
        }

        if (myScrollRect.verticalNormalizedPosition <= 0.01f && !character_panel.activeSelf && !has_started_talking) // Scroll View가 맨 끝에 도달했을 때
        {
            // 주인공 대화창 Panel을 활성화
            character_panel.SetActive(true);

            // 주인공 대사 이벤트 발생
            StartCoroutine(startCharacterTalk());
            has_started_talking = true; // 대화가 시작되었음을 표시
        }

    }

    IEnumerator startCharacterTalk()
    {

        // 첫 번째 대사 표시
        dialogue_text.text = "주인공: 기억을 지워…? 내가 기억을 지우는 일을 했다는 건가?";
        yield return new WaitForSeconds(delay_between_lines);
            
        // 대사 사라짐
        dialogue_text.text = "";

        // 두 번째 대사 표시
        yield return new WaitForSeconds(0.5f); // 대사 간의 짧은 딜레이
        dialogue_text.text = "주인공: …그랬던 것 같기도 한데, 자세한 것들이 하나도 기억나지 않아.";
        yield return new WaitForSeconds(delay_between_lines);

        // 대사 사라짐
        dialogue_text.text = "";

        // 세 번째 대사 표시
        yield return new WaitForSeconds(0.5f); // 대사 간의 짧은 딜레이
        dialogue_text.text = "주인공: 이 ‘의뢰’를 해결하면 무언가 더 기억이 날까…";
        yield return new WaitForSeconds(delay_between_lines);

        // 대사 사라짐
        dialogue_text.text = "";

        // 주인공 대화창 Panel 비활성화
        character_panel.SetActive(false);

        // Accept 버튼 활성화
        accept_button.gameObject.SetActive(true);
        accept_button.Select(); // Accept 버튼을 디폴트로 선택하도록 만듦
        
    }

   

}

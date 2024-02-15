using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event3Talk : MonoBehaviour
{
    public ScrollRect myScrollRect; // Scroll View의 Scroll Rect를 연결
    public float scrollSpeed = 0.4f; // 스크롤 속도 조절

    public Button accept_button; // Accept 버튼

    public PlayerController player_controller;

    [SerializeField] bool scroll_end = false; // 스크롤 끝 확인

    TalkAction action;
    
    void Start()
    {
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        accept_button.gameObject.SetActive(false);
        action = FindObjectOfType<TalkAction>();

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

        if (Input.GetKeyDown(KeyCode.Space) && !action.is_talking) // Space 키를 누르면
        {
            // Scroll View의 위치를 맨 끝으로 이동
            myScrollRect.verticalNormalizedPosition = 0;
            
         
        }

        
        if (myScrollRect.verticalNormalizedPosition <= 0.01f && GameManager.instance.story_info == 3 && !scroll_end) // Scroll View가 맨 끝에 도달했을 때
        {
            scroll_end = true;
            action.Action();

        }

        if (GameManager.instance.story_info == 4)
        {
            // Accept 버튼 활성화
            accept_button.gameObject.SetActive(true);
            accept_button.Select(); // Accept 버튼을 디폴트로 선택하도록 만듦
        }
        


    }

    
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event4Button : MonoBehaviour
{

    public GameObject dog_request_panel;

    public Button accept_button;

    TalkAction action;


    void Start()
    {
        action = FindObjectOfType<TalkAction>();
        accept_button.onClick.AddListener(OnButtonClick); // 버튼에 클릭 이벤트 리스너 추가

    }

    // 버튼 클릭시 실행할 함수
    public void OnButtonClick()
    {
        dog_request_panel.SetActive(false);
        action.Action();
        
    }

}

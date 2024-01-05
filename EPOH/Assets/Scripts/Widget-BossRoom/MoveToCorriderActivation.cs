using UnityEngine;
using UnityEngine.UI;

public class MoveToCorriderActivation : MonoBehaviour
{
    public Button mission1_button; // Mission1 버튼
    public Button mission2_button; // Mission2 버튼
    public Button mission3_button; // Mission3 버튼
    public Button MoveToCorrider_button; // MoveToCorrider 버튼

    private Button lastClickedButton; // 마지막에 클릭된 버튼을 저장하는 변수

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        mission1_button.onClick.AddListener(() => OnMissionButtonClick(mission1_button));
        mission2_button.onClick.AddListener(() => OnMissionButtonClick(mission2_button));
        mission3_button.onClick.AddListener(() => OnMissionButtonClick(mission3_button));
    }

    // 임무 버튼 클릭 시 호출되는 함수
    void OnMissionButtonClick(Button clickedButton)
    {
        // 마지막에 클릭된 버튼 갱신
        lastClickedButton = clickedButton;

        // MoveToCorrider_button 버튼 활성화
        MoveToCorrider_button.interactable = true;
    }

    // MoveToCorrider 버튼 클릭 시 호출되는 함수
    void OnMoveToCorriderButtonClick()
    {
        if (lastClickedButton == mission1_button)
        {
            Debug.Log("Mission 1 버튼이 클릭되었습니다.");
            // Mission 1에 대한 추가적인 처리
        }
        else if (lastClickedButton == mission2_button)
        {
            Debug.Log("Mission 2 버튼이 클릭되었습니다.");
            // Mission 2에 대한 추가적인 처리
        }
        else if (lastClickedButton == mission3_button)
        {
            Debug.Log("Mission 3 버튼이 클릭되었습니다.");
            // Mission 3에 대한 추가적인 처리
        }

        // 나머지 처리 및 원하는 동작 수행
    }
}

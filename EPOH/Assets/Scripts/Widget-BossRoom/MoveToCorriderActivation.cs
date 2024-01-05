using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveToCorriderActivation : MonoBehaviour
{
    public Button mission1_button; // Mission1 버튼
    public Button mission2_button; // Mission2 버튼
    public Button mission3_button; // Mission3 버튼
    public Button MoveToCorrider_button; // MoveToCorrider 버튼

    public GameObject mission_decision_popup; // 팝업 창
    public Button yesButton; // 'Yes' 버튼
    public Button noButton; // 'No' 버튼

    private Button lastClickedButton; // 마지막에 클릭된 버튼을 저장하는 변수
    private BossSelection bossSelection; // BossSelection 스크립트에 대한 참조 변수


    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        mission1_button.onClick.AddListener(() => OnMissionButtonClick(mission1_button));
        mission2_button.onClick.AddListener(() => OnMissionButtonClick(mission2_button));
        mission3_button.onClick.AddListener(() => OnMissionButtonClick(mission3_button));

        // 'Yes', 'No' 버튼에 클릭 이벤트 리스너 추가
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);

        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // BossSelection 스크립트에 대한 참조 가져오기
        bossSelection = FindObjectOfType<BossSelection>();
    }

    // 임무 버튼 클릭 시 호출되는 함수
    void OnMissionButtonClick(Button clickedButton)
    {
        // 마지막에 클릭된 버튼 갱신
        lastClickedButton = clickedButton;

        // MoveToCorrider_button 버튼 활성화
        MoveToCorrider_button.interactable = true;

        // 팝업 창 활성화
        mission_decision_popup.SetActive(true);

        // BossSelection 스크립트의 선택된 보스 인덱스 참고하여 문구 출력
        int bossIndex = bossSelection.GetSelectedBossIndex(); // BossSelection 스크립트의 선택된 보스 인덱스 가져오기
        TextMeshProUGUI popupText = mission_decision_popup.GetComponentInChildren<TextMeshProUGUI>(); // 팝업 창의 텍스트 가져오기
        popupText.text = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text + " 임무를 선택하시겠습니까?";
    }

    // 'Yes' 버튼 클릭 시 호출되는 함수
    void OnYesButtonClick()
    {
        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // 모든 미션 버튼 비활성화
        mission1_button.interactable = false;
        mission2_button.interactable = false;
        mission3_button.interactable = false;
    }

    // 'No' 버튼 클릭 시 호출되는 함수
    void OnNoButtonClick()
    {
        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // 마지막에 클릭된 버튼 초기화
        lastClickedButton = null;
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

    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionDecisionCorrider : MonoBehaviour
{
    public Button mission1_button; // Mission1 버튼
    public Button mission2_button; // Mission2 버튼
    public Button mission3_button; // Mission3 버튼

    public GameObject mission_decision_popup; // 팝업 창
    public Button yes_button; // 'Yes' 버튼
    public Button no_button; // 'No' 버튼

    private Button last_clicked_button; // 마지막에 클릭된 버튼을 저장하는 변수
    private BossSelection boss_selection; // BossSelection 스크립트에 대한 참조 변수
    public bool canInteractWithPortal = false;

    void Start()
    {
        // 각 버튼에 클릭 이벤트 리스너 추가
        mission1_button.onClick.AddListener(() => onMissionButtonClick(mission1_button));
        mission2_button.onClick.AddListener(() => onMissionButtonClick(mission2_button));
        mission3_button.onClick.AddListener(() => onMissionButtonClick(mission3_button));

        // 'Yes', 'No' 버튼에 클릭 이벤트 리스너 추가
        yes_button.onClick.AddListener(onYesButtonClick);
        no_button.onClick.AddListener(onNoButtonClick);

        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // BossSelection 스크립트에 대한 참조 가져오기
        boss_selection = FindObjectOfType<BossSelection>();

    }

    // 임무 버튼 클릭 시 호출되는 함수
    public void onMissionButtonClick(Button clicked_button)
    {
        // 마지막에 클릭된 버튼 갱신
        last_clicked_button = clicked_button;


        // 팝업 창 활성화
        mission_decision_popup.SetActive(true);

        // BossSelection 스크립트의 선택된 보스 인덱스 참고하여 문구 출력
        int boss_index = boss_selection.getSelectedBossIndex(); // BossSelection 스크립트의 선택된 보스 인덱스 가져오기
        TextMeshProUGUI popup_text = mission_decision_popup.GetComponentInChildren<TextMeshProUGUI>(); // 팝업 창의 텍스트 가져오기
        popup_text.text = clicked_button.GetComponentInChildren<TextMeshProUGUI>().text + " 임무를 선택하시겠습니까?";
    }

    // 'Yes' 버튼 클릭 시 호출되는 함수
    public void onYesButtonClick()
    {
        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // 모든 미션 버튼 비활성화
        mission1_button.interactable = false;
        mission2_button.interactable = false;
        mission3_button.interactable = false;

        canInteractWithPortal = true;

    }

    // 'No' 버튼 클릭 시 호출되는 함수
    public void onNoButtonClick()
    {
        // 팝업 창 비활성화
        mission_decision_popup.SetActive(false);

        // 마지막에 클릭된 버튼 초기화
        last_clicked_button = null;

        canInteractWithPortal = false;

    }


    // Portal과 상호작용시 호출되는 함수
    public void onPortalInteraction()
    {
        if(canInteractWithPortal)
        {
            if (last_clicked_button == mission1_button)
            {
                Debug.Log("Mission 1 버튼이 클릭되었습니다.");
                // Mission 1에 대한 추가적인 처리
            }
            else if (last_clicked_button == mission2_button)
            {
                Debug.Log("Mission 2 버튼이 클릭되었습니다.");
                // Mission 2에 대한 추가적인 처리
            }
            else if (last_clicked_button == mission3_button)
            {
                Debug.Log("Mission 3 버튼이 클릭되었습니다.");
                // Mission 3에 대한 추가적인 처리
            }
        }
        else
        {
            Debug.Log("상호작용이 불가능한 상태입니다.");
        }

    }
}
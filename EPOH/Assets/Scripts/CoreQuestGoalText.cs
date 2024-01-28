using UnityEngine;
using TMPro;
public class CoreQuestGoalText : MonoBehaviour
{
    public MissionDecisionCorrider mission_decision_corrider; //  MissionDecisionCorrider 스크립트 참조
    public GameManager game_manager; // GameManager 스크립트 참조

    public TextMeshProUGUI quest_text; // 퀘스트 목표 및 달성 현황이 표기된 텍스트
    public GameObject quest_box;


    // Start is called before the first frame update
    void Start()
    {
        mission_decision_corrider = GetComponent<MissionDecisionCorrider>();

        if (mission_decision_corrider == null)
        {
            Debug.LogError("MissionDecisionCorrider 스크립트가 할당되지 않았습니다.");
        }

        quest_text = quest_box.GetComponentInChildren<TextMeshProUGUI>();
        
        if (quest_text == null)
        {
            Debug.LogError("퀘스트 목표 및 달성 현황이 표기된 텍스트가 할당되지 않았습니다.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mission_decision_corrider.last_clicked_button != null)
        {
            // 완료한 퀘스트 이름을 표기
            if (GameManager.instance.boss_clear_info[GameManager.instance.boss_num])
            {
                quest_text.text = mission_decision_corrider.last_clicked_button.GetComponentInChildren<TextMeshProUGUI>().text + " 퀘스트를 달성하였습니다!";
            }

            // 선택한(완료 전) 퀘스트 이름을 표기
            quest_text.text = mission_decision_corrider.last_clicked_button.GetComponentInChildren<TextMeshProUGUI>().text + " 퀘스트를 진행 중입니다";
        }


        else 
        {
            // 미션 버튼을 선택하기 전일 경우
            quest_text.text = "아직 퀘스트를 시작하지 않았습니다.";
        }
        
    }
}

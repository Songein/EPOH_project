using UnityEngine;
using TMPro;
public class CoreQuestGoalText : MonoBehaviour
{
    public TextMeshProUGUI quest_text; // 퀘스트 목표 및 달성 현황이 표기된 텍스트
    public GameObject quest_box;


    // Start is called before the first frame update
    void Start()
    {
        quest_text = quest_box.GetComponentInChildren<TextMeshProUGUI>();
        
        if (quest_text == null)
        {
            Debug.LogError("퀘스트 목표 및 달성 현황이 표기된 텍스트가 할당되지 않았습니다.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.boss_num >= 0)
        {
            /*
            // 완료한 퀘스트 이름을 표기
            if (GameManager.instance.boss_clear_info[GameManager.instance.boss_num])
            {
                quest_text.text = GameManager.instance.boss_info[GameManager.instance.boss_num,0] + " 퀘스트를 달성하였습니다!";
            }

            // 선택한(완료 전) 퀘스트 이름을 표기
            quest_text.text = GameManager.instance.boss_info[GameManager.instance.boss_num, 0] + " 퀘스트를 진행 중입니다";
            */
        }
        else 
        {
            // 미션 버튼을 선택하기 전일 경우
            quest_text.text = "아직 퀘스트를 시작하지 않았습니다.";
        }
        
    }
}

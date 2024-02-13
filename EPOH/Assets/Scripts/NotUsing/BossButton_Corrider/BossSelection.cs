using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BossSelection : MonoBehaviour
{
    /*
    public Button[] boss_buttons; // 보스를 선택하는 버튼 배열
    private GameManager game_manager; // GameManager 스크립트에 대한 참조 변수

    private int selected_boss_index; // 선택된 보스 인덱스를 저장하는 변수

    void Start()
    {
        // 랜덤으로 보스 정보 배정
        assignBossInfoToButtons();
    }

    public void assignBossInfoToButtons()
    {
        List<int> assigned_boss_indexes = new List<int>(); // 이미 배정된 보스 인덱스를 추적하기 위한 리스트

        // 보스 정보를 버튼에 배정
        for (int i = 0; i < boss_buttons.Length; i++)
        {
            int random_boss_index;

            // 중복된 보스가 배정되지 않도록 처리
            do
            {
                random_boss_index = Random.Range(0, GameManager.boss_cnt); // 랜덤한 보스 인덱스 선택
            } while (assigned_boss_indexes.Contains(random_boss_index));

            // GameManager의 boss_clear_info를 확인하여 true인 보스는 배정하지 않음
            while (GameManager.instance.boss_clear_info[random_boss_index])
            {
                random_boss_index = (random_boss_index + 1) % GameManager.boss_cnt; // 다음 보스로 이동
            }

            assigned_boss_indexes.Add(random_boss_index); // 배정된 보스 인덱스를 리스트에 추가
            boss_buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.boss_info[random_boss_index, 0]; // 보스 이름으로 버튼 텍스트 설정

            // 버튼 클릭 이벤트 설정
            int button_index = i; // 클로저에 전달하기 위해 인덱스 값 저장
            boss_buttons[i].onClick.AddListener(() => onBossButtonClick(button_index, random_boss_index));
        }
    }

    // 버튼이 클릭될 때 실행되는 함수
    public void onBossButtonClick(int button_index, int boss_index)
    {
        selected_boss_index = boss_index; // 선택한 보스의 인덱스 기록
        GameManager.instance.boss_num = boss_index; // GameManager에 선택한 보스 정보 전달
        Debug.Log("사용자가 " + boss_buttons[button_index].GetComponentInChildren<TextMeshProUGUI>().text + "를 선택했습니다.");
    }

    // 선택된 보스 인덱스를 반환하는 메서드
    public int getSelectedBossIndex()
    {
        return selected_boss_index;
    }
    */
}
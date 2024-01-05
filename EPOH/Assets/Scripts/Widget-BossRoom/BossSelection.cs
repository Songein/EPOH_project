using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BossSelection : MonoBehaviour
{
    public Button[] bossButtons; // 보스를 선택하는 버튼 배열
    private GameManager gameManager; // GameManager 스크립트에 대한 참조 변수

    private int selectedBossIndex; // 선택된 보스 인덱스를 저장하는 변수


    void Start()
    {
        // GameManager 스크립트에 대한 참조 가져오기
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다.");
            return;
        }

        // 랜덤으로 보스 정보 배정
        AssignBossInfoToButtons();
    }

    public void AssignBossInfoToButtons()
    {
        List<int> assignedBossIndexes = new List<int>(); // 이미 배정된 보스 인덱스를 추적하기 위한 리스트

        // 보스 정보를 버튼에 배정
        for (int i = 0; i < bossButtons.Length; i++)
        {
            int randomBossIndex;

            // 중복된 보스가 배정되지 않도록 처리
            do
            {
                randomBossIndex = Random.Range(0, GameManager.boss_cnt); // 랜덤한 보스 인덱스 선택
            } while (assignedBossIndexes.Contains(randomBossIndex));

            // GameManager의 boss_clear_info를 확인하여 true인 보스는 배정하지 않음
            while (gameManager.boss_clear_info[randomBossIndex])
            {
                randomBossIndex = (randomBossIndex + 1) % GameManager.boss_cnt; // 다음 보스로 이동
            }

            assignedBossIndexes.Add(randomBossIndex); // 배정된 보스 인덱스를 리스트에 추가
            bossButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = gameManager.boss_info[randomBossIndex, 0]; // 보스 이름으로 버튼 텍스트 설정

            // 버튼 클릭 이벤트 설정
            int buttonIndex = i; // 클로저에 전달하기 위해 인덱스 값 저장
            bossButtons[i].onClick.AddListener(() => OnBossButtonClick(buttonIndex, randomBossIndex));
        }
    }


    // 버튼이 클릭될 때 실행되는 함수
    public void OnBossButtonClick(int buttonIndex, int bossIndex)
    {
        gameManager.boss_num = bossIndex; // 선택한 보스의 인덱스 기록
        Debug.Log("사용자가 " + bossButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text + "를 선택했습니다.");
        
    }
    
     // 선택된 보스 인덱스를 반환하는 메서드
    public int GetSelectedBossIndex()
    {
        return selectedBossIndex;
    }
}

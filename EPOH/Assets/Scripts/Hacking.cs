using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : MonoBehaviour
{
    public BossManager boss_manager; // BossManager 스크립트에 대한 참조
    public GameManager game_manager; // GameManager 스크립트에 대한 참조

    private void Start()
    {
        // BossManager 스크립트와 GameManager 스크립트 참조
        boss_manager = GetComponent<BossManager>();
        game_manager = FindObjectOfType<GameManager>();
    }

    // BossManager의 hacking_point가 100%에 다다랐을 시 보스전이 종료되는 함수
    public void checkHackingPoint()
    {
        float hacking_percentage = (float)boss_manager.hacking_point / 100; // hacking_point의 백분율 계산

        if (hacking_percentage >= 1.0f)
        {
            endBossBattle();
            updateBossClearInfo();
        }
    }

    // 보스전 종료 함수
    private void endBossBattle()
    {
        // 보스전 종료 관련 작업 수행

        Debug.Log("보스전 종료");
    }

    // 보스 클리어 정보 갱신
    private void updateBossClearInfo()
    {
        int boss_index = game_manager.boss_num; // 선택한 보스의 인덱스

        if (boss_index >= 0 && boss_index < GameManager.boss_cnt)
        {
            game_manager.boss_clear_info[boss_index] = true; // 보스 클리어 여부 업데이트

            Debug.Log("보스 클리어!");
            
        }
    }

    // hacking_point를 증가시키는 함수
    public void increaseHackingPoint(int amount)
    {
        boss_manager.hacking_point += amount;

        // hacking_point 증가 후 확인
        checkHackingPoint();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacking : MonoBehaviour
{
    public BossManager boss_manager; // BossManager 스크립트에 대한 참조
    public BossHealth boss_health;

    private GameObject boss;

    private BossDogScene boss_dog_scene;

    private void Start()
    {
        
        boss = GameObject.FindWithTag("Boss");
        boss_dog_scene = FindObjectOfType<BossDogScene>();

        if (boss != null)
        {
            // BossManager 스크립트 참조
            boss_manager = boss.GetComponent<BossManager>();

            if (boss_manager == null)
            {
                Debug.LogError("[Hacking] : BossManager 스크립트를 찾을 수 없습니다.");
            }

            boss_health = boss.GetComponent<BossHealth>();
            if (boss_health == null)
            {
                Debug.LogError("[Hacking] : BossHealth 스크립트를 찾을 수 없습니다.");
            }
        }
         else
        {
            Debug.LogError("[Hacking] : Boss GameObject를 찾을 수 없습니다. 'Boss' 태그를 가진 GameObject가 씬에 있는지 확인하세요.");
        }
    }

    // BossManager의 hacking_point가 200에 다다랐을 시 보스전이 종료되는 함수
    public void checkHackingPoint()
    {
        if (boss_manager.hacking_point == 200 && boss_manager.boss_hp <= 0)
        {
            endBossBattle();
        }
    }

    // 보스전 종료 함수
    public void endBossBattle()
    {
        // hacking_point= 200 이 되고 boss_hp = 0 이 되면 임무완료 씬으로 이동
        //SceneManager.LoadScene("MissionClear");
        boss_dog_scene.CompleteHacking();

        Debug.Log("보스전 종료");
    }

    // 보스 클리어 정보 갱신
    public void updateBossClearInfo()
    {
        if (GameManager.instance.boss_num < GameManager.boss_cnt)
        {
            GameManager.instance.boss_num++; // 보스 클리어 여부 업데이트
        }
        Debug.Log("보스 클리어!");
    }

    // hacking_point를 증가시키는 함수
    public void increaseHackingPoint(int amount)
    {
        if (boss_manager != null)
        {
            // 해킹포인트가 200 초과하지 않음
            if (boss_manager.hacking_point < 200)
            {
                boss_manager.hacking_point += amount;
                boss_manager.hacking_point = Mathf.Min(boss_manager.hacking_point, 200);

                // hacking_point 증가 후 확인
                checkHackingPoint();
            }
        }
        else
        {
            Debug.LogError("[Hacking] : BossManager 컴포넌트를 찾을 수 없습니다.");
        }
    }

    // hacking_point를 감소시키는 함수
    public void decreaseHackingPoint(int amount)
    {
        if (boss_manager != null)
        {
            // 해킹포인트가 0 보다 작지 않음
            if (boss_manager.hacking_point > 0)
            {
                boss_manager.hacking_point -= amount;
                boss_manager.hacking_point = Mathf.Max(boss_manager.hacking_point, 0);

                // hacking_point 감소 후 확인
                checkHackingPoint();
            }
        }
        else
        {
            Debug.LogError("[Hacking] : BossManager 컴포넌트를 찾을 수 없습니다.");
        }
    }

    // Boss 공격 성공시 hacking_point 증가
    public void onBossHealthDecrease(float damage)
    {

        if (boss_health != null)
        {
            // 보스 공격 성공시마다 hacking_point 10씩 증가
            increaseHackingPoint(5);
            
        }
        else
        {
            Debug.LogError("[Hacking] : BossHealth 컴포넌트를 찾을 수 없습니다.");
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossForgetMeNotScene : MonoBehaviour
{
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    public BossManager boss_manager;
    public PlayerController player_controller;
    public Hacking hacking;
    public bool hacking_complete; // 해킹 완료

    [SerializeField] private GameObject phase1_object_prefab; //페이즈1 오브젝트 프리팹
    [SerializeField] private GameObject phase2_object_prefab; //페이즈2 오브젝트 프리팹
    [SerializeField] private GameObject phase3_object_prefab; //페이즈3 오브젝트 프리팹

    private GameObject phase1_object_instance; //페이즈1 오브젝트 인스턴스
    private GameObject phase2_object_instance; //페이즈2 오브젝트 인스턴스
    private GameObject phase3_object_instance; //페이즈2 오브젝트 인스턴스

    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (boss != null)
        {
            boss_manager = boss.GetComponent<BossManager>();
            hacking = boss.GetComponent<Hacking>();
        }
        
        boss_manager.battle_start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_manager == null || hacking == null)
        {
            Debug.LogError("BossManager or Hacking component is missing.");
            return;
        }

        if (boss_manager.battle_start && !boss_manager.phase1_start && boss_manager.hacking_point == 50) // 페이즈 1 시작
        {
            /*
            //보스 움직임 멈춤(배틀 일시정지)
            boss_manager.battle_start = false;
            boss_manager.phase1_start = true;
            bossForgetMeNotPhase1();
            */
            if (phase1_object_instance == null)
            {
                Debug.Log("Phase 1 start condition met");
                phase1_object_instance = Instantiate(phase1_object_prefab, new Vector3(-5, -3, 0), Quaternion.identity);

            }
        }

        if (boss_manager.battle_start && !boss_manager.phase2_start && boss_manager.hacking_point == 100) // 페이즈 2 시작
        {
            /*
            //보스 움직임 멈춤(배틀 일시정지)
            boss_manager.battle_start = false;
            boss_manager.phase2_start = true;
            bossForgetMeNotPhase2();
            */
            if (phase2_object_instance == null)
            {
                Debug.Log("Phase 2 start condition met");
                phase2_object_instance = Instantiate(phase2_object_prefab, new Vector3(-5, -3, 0), Quaternion.identity);

            }

        }

        if (boss_manager.battle_start && !boss_manager.phase3_start && boss_manager.hacking_point == 150) // 페이즈 3 시작
        {
            /*
            //보스 움직임 멈춤(배틀 일시정지)
            boss_manager.battle_start = false;
            boss_manager.phase3_start = true;
            bossForgetMeNotPhase3();
            */
            if (phase3_object_instance == null)
            {
                Debug.Log("Phase 3 start condition met");
                phase3_object_instance = Instantiate(phase3_object_prefab, new Vector3(-5, -3, 0), Quaternion.identity);

            }
        }
    
        if (boss_manager.battle_start && boss_manager.hacking_point == 200)
        {
            boss_manager.battle_start = false;
            GameManager.instance.bossClearInfo[2] = false;
            GameManager.instance.bossClearInfo[3] = true;

            bossForgetMeNotMissionClear();

            //hacking.endBossBattle();

            Debug.Log("Boss ForgetMeNot 클리어!");
        }

        if (boss_manager.battle_start && boss_manager.player_hp == 0)
        {
            playerDeath();
        }
        
    }

    void playerDeath()
    {
        boss_manager.battle_start = false;
        SceneManager.LoadScene("OfficeRoom2");
        
    }

    void bossForgetMeNotPhase1()
    {
        player_health.is_invincible = true;
        Debug.Log("Boss ForgetMeNot 페이즈1 연출");
    }

    void bossForgetMeNotPhase2()
    {
        player_health.is_invincible = true;
        Debug.Log("Boss ForgetMeNot 페이즈2 연출");
    }

    void bossForgetMeNotPhase3()
    {
        player_health.is_invincible = true;
        Debug.Log("Boss ForgetMeNot 페이즈3 연출");
    }

    void bossForgetMeNotMissionClear()
    {
        Debug.Log("Boss ForgetMeNot 미션 클리어 연출");
    }

}

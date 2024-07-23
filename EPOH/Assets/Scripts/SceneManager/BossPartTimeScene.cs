using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPartTimeScene : MonoBehaviour
{
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    public BossManager boss_manager;
    public PlayerController player_controller;
    public Hacking hacking;
    public bool hacking_complete; // 해킹 완료

    [SerializeField] private GameObject phase1_object; //페이즈1 오브젝트
    [SerializeField] private GameObject phase2_object; //페이즈2 오브젝트

    private GameObject boss;

    private bool space_pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player_health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        phase1_object.SetActive(false);
        phase2_object.SetActive(false);

        if (boss != null)
        {
            boss_manager = boss.GetComponent<BossManager>();
            hacking = boss.GetComponent<Hacking>();
        }
        
        boss_manager.battle_start = true;
        hacking_complete = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_manager == null || hacking == null)
        {
            // Prevent further execution if dependencies are not set
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (phase1_object.activeSelf || phase2_object.activeSelf))
        {
            space_pressed = true;
        }

        if (boss_manager.battle_start && !boss_manager.phase1_start && boss_manager.hacking_point == 70) // 페이즈 1 시작
        {
            //보스 움직임 멈춤(배틀 일시정지)
            boss_manager.battle_start = false;
            boss_manager.phase1_start = true;
            bossPartTimePhase1();
        }

        if (boss_manager.battle_start && !boss_manager.phase2_start && boss_manager.hacking_point == 140) // 페이즈 2 시작
        {
            //보스 움직임 멈춤(배틀 일시정지)
            boss_manager.battle_start = false;
            boss_manager.phase2_start = true;
            bossPartTimePhase2();
        }
    
        if (boss_manager.battle_start && boss_manager.hacking_point == 200)
        {
            boss_manager.battle_start = false;
            GameManager.instance.boss_clear_info[1] = false;
            GameManager.instance.boss_clear_info[2] = true;

            bossPartTimeMissionClear();

            hacking.endBossBattle();

            Debug.Log("Boss PartTime 클리어!");
        }

        if (boss_manager.battle_start && boss_manager.player_hp == 0)
        {
            playerDeath();
        }
    }

    void playerDeath()
    {
        boss_manager.battle_start = false;
        SceneManager.LoadScene("OfficeRoom1");
        
    }

     void bossPartTimePhase1()
    {
        player_health.is_invincible = true;
        Debug.Log("Boss PartTime 페이즈1 연출");
        StartCoroutine(showPhaseObject());
    }

    void bossPartTimePhase2()
    {
        player_health.is_invincible = true;
        Debug.Log("Boss PartTime 페이즈2 연출");
        StartCoroutine(showPhaseObject());
    }

    void bossPartTimeMissionClear()
    {
        Debug.Log("Boss PartTime 미션 클리어 연출");
    }

    IEnumerator showPhaseObject()
    {
        player_controller.is_interacting = true;
        yield return new WaitForSeconds(0.5f);
        if (boss_manager.phase1_start && !boss_manager.phase2_start)
        {
            phase1_object.SetActive(true);
        }
        else if (boss_manager.phase2_start)
        {
            phase2_object.SetActive(true);
            
        }

        yield return StartCoroutine(waitForKeyPress());

        phase1_object.SetActive(false);
        phase2_object.SetActive(false);
        player_controller.is_interacting = false;
        player_health.is_invincible = false;
        boss_manager.battle_start = true;
    }

    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사, 오브젝트로 넘어가는 함수
    {
        while (!space_pressed)
        {
            yield return null;
        }
        space_pressed = false; // Space 키를 눌렀다는 체크를 초기화
    }
}

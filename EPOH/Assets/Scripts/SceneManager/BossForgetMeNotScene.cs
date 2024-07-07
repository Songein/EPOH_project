using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForgetMeNotScene : MonoBehaviour
{
    public BossManager boss_manager;
    public Hacking hacking;
    public bool battle_start; // 배틀 시작
    public bool hacking_complete; // 해킹 완료

    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");

        if (boss != null)
        {
            boss_manager = boss.GetComponent<BossManager>();
            hacking = boss.GetComponent<Hacking>();
        }
        
        battle_start = true;
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
    
        if (battle_start && boss_manager.hacking_point == 200)
        {
            battle_start = false;
            GameManager.instance.boss_clear_info[2] = false;
            GameManager.instance.boss_clear_info[3] = true;

            missionClear();

            hacking.endBossBattle();

            Debug.Log("Boss ForgetMeNot 클리어!");
        }

        if (battle_start && boss_manager.player_hp == 0)
        {
            playerDeath();
        }
        
    }

    void playerDeath()
    {
        battle_start = false;
        SceneManager.LoadScene("OfficeRoom2");
        
    }

    void phase1()
    {
        Debug.Log("Boss ForgetMeNot 페이즈1 연출");
    }

    void phase2()
    {
        Debug.Log("Boss ForgetMeNot 페이즈2 연출");
    }

    void phase3()
    {
        Debug.Log("Boss ForgetMeNot 페이즈3 연출");
    }

    void missionClear()
    {
        Debug.Log("Boss ForgetMeNot 미션 클리어 연출");
    }
}

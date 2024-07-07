using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDogScene : MonoBehaviour
{
    public BossManager boss_manager;
    public Hacking hacking;
    public bool battle_start; // 배틀 시작
    public bool hacking_complete; // 해킹 완료



    // Start is called before the first frame update
    void Start()
    {
        boss_manager = GetComponent<BossManager>();
        hacking = GetComponent<Hacking>();
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
    
        if (battle_start && boss_manager.boss_hp == 0 && boss_manager.hacking_point == 200)
        {
            battle_start = false;
            GameManager.instance.office_room = 1;
            
            SceneManager.LoadScene("OfficeRoom" + GameManager.instance.office_room);

            //hacking.endBossBattle();
            Debug.Log("Boss Dog 클리어!");
        }

        if (battle_start && boss_manager.player_hp == 0)
        {
            playerDeath();
        }
    }


    public void playerDeath()
    {
        battle_start = false;
        SceneManager.LoadScene("OfficeRoom1");
        
    }
}

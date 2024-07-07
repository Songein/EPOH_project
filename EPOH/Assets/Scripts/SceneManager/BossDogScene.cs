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




    // Start is called before the first frame update
    void Start()
    {
        battle_start = true;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (battle_start && boss_manager.hacking_point == 200)
        {
            battle_start = false;
            GameManager.instance.office_room = 1;
            hacking.endBossBattle();
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

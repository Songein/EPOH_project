using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public BossManager boss_manager;
    public Hacking hacking;


    private GameObject boss;
    public float boss_hp = 1000f; //보스의 목숨

    public void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        boss_manager = boss.GetComponent<BossManager>();
        hacking = GetComponent<Hacking>();
    }
    
    public void Damage(float power)
    {
        // Ensure boss_hp doesn't go below 0
        if (boss_hp > 0)
        {
        
            boss_hp -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
            if (boss_hp <= 0)
            {
                Die();
                boss_manager.boss_hp = boss_hp;
                hacking.endBossBattle();
            }
            else
            { 
                Debug.Log("[Enemy] 남은 목숨 : " + boss_hp);
                boss_manager.boss_hp = boss_hp;

            }
        }
       
    }

    void Die()
    {
        Debug.Log("[Enemy] : " + gameObject.name + " 사망");
        gameObject.SetActive(false);
    }
}

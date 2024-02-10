using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth :MonoBehaviour
{
    public BossManager boss_manager;
    public Hacking hacking;
    private SpriteRenderer sr; //보스의 SpriteRenderer 참조

    private GameObject boss;
    public float boss_hp = 1000f; //보스의 목숨

    public void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        boss_manager = boss.GetComponent<BossManager>();
        hacking = GetComponent<Hacking>();
        sr = GetComponent<SpriteRenderer>(); //보스의 SpriteRenderer 할당
    }
    
    public void Damage(float power)
    {
        // Ensure boss_hp doesn't go below 0
        if (boss_hp > 0)
        {
        
            boss_hp -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
            sr.color = new Color(1, 0, 0, 0.6f); // 보스의 스프라이트 색을 투명한 빨강색으로 변경
            Invoke("ReturnColor",1f);
            if (boss_hp <= 0)
            {
                Die();
                boss_hp = 0;
                boss_manager.boss_hp = boss_hp;
                hacking.checkHackingPoint();
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
        //gameObject.SetActive(false);
    }

    void ReturnColor()
    {
        //보스의 스프라이트 색 원래대로 회복
        sr.color = new Color(1, 1, 1, 1f);
    }
}

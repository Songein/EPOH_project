using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float boss_hp = 2000f; //보스의 목숨
    
    public void Damage(float power)
    {
        boss_hp -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
        if (boss_hp <= 0)
        {
            Die();
        }
        else
        { 
            Debug.Log("[Enemy] 남은 목숨 : " + boss_hp);
        }
       
    }

    void Die()
    {
        Debug.Log("[Enemy] : " + gameObject.name + " 사망");
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health = 20f; //최대 목숨
    private float cur_health; //현재 목숨

    void Start()
    {
        cur_health = max_health; //현재 목숨을 최대 목숨으로 초기화
    }
    public void Damage(float power)
    {
        cur_health -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
        if (cur_health <= 0)
        {
            Die();
        }
        else
        { 
            Debug.Log("[Enemy] 남은 목숨 : " + cur_health);
        }
       
    }

    void Die()
    {
        Debug.Log("[Enemy] : " + gameObject.name + " 사망");
        gameObject.SetActive(false);
    }
}

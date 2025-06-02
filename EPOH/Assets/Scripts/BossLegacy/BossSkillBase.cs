using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillBase : MonoBehaviour
{
    //스킬 세기
    [SerializeField] protected float _power;
    
    //플레이어와 충돌 시 스킬 세기만큼 플레이어에게 데미지를 입힘
    protected void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Damage(_power);
        }
    }
}

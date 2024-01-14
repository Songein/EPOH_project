using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    public float attack_power = 30f; //보스의 공격 세기
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //플레이어와 충돌할 경우 공격으로 임의 설정
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("[BossAttack] : 플레이어를 공격하였습니다.");
            //PlayerHealth 스크립트 할당
            player_health = other.gameObject.GetComponent<PlayerHealth>();
            player_health.Damage(attack_power); //보스의 공격 세기만큼 플레이어의 hp 감소
        }
    }
}

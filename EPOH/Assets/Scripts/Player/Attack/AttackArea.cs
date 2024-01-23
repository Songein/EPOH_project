using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float attack_power; //공격 세기
    private CircleCollider2D collider; //AttackArea의 collider;
    private BossHealth boss_health; //BossHealth 참조

    void Awake()
    {
        //공격 범위 콜라이더 할당
        collider = GetComponent<CircleCollider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //공격 범위와 트리거 충돌 주체의 태그가 Enemy이면
        if (other.CompareTag("Boss"))
        {
            Debug.Log("[AttackArea] : 충돌");
            //상대의 Enemy 스크립트 참조
            boss_health = other.GetComponent<BossHealth>();
            //상대를 공격하기
            boss_health.Damage(attack_power);
        }
        else
        {
            //Debug.Log("[AttackArea] : 충돌 주체가 적이 아님");
        }
    }

    //공격 세기 설정 함수
    public void SetAttackPower(float power)
    {
        Debug.Log("[AttackArea] : 공격 세기를 " + power + "로 설정");
        attack_power = power;
    }

    //플레이어 이동에 따라 공격범위 뒤집기
    public void Flip(bool is_facing_right)
    {
        //플레이어가 오른쪽을 쳐다보고 있으면 collider offset의 x 값을 1, 아니면 -1로 설정
        collider.offset = new Vector2(is_facing_right ? 1f : -1f, collider.offset.y);
    }

    
}
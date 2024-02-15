using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteArea : MonoBehaviour
{
    [SerializeField] float power = 10f; //물기 공격 세기
    private CircleCollider2D collider; //공격 범위 콜라이더

    void Start()
    {
        //콜라이더 할당
        collider = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //트리거 충돌 대상이 플레이어라면
        if (other.CompareTag("Player"))
        {
            //플레이어 hp를 관리하는 스크립트 할당하여 Damage 메서드 접근
            PlayerHealth player_health = other.GetComponent<PlayerHealth>();
            player_health.Damage(power);
        }
    }

    //플레이어 위치에 따른 공격 범위 설정
    public void SetPos(bool is_player_right)
    {
        if (is_player_right)
        {
            collider.offset = new Vector2(0.98f, collider.offset.y);
        }
        else
        {
            collider.offset = new Vector2(-0.98f, collider.offset.y);
        }
    }
}

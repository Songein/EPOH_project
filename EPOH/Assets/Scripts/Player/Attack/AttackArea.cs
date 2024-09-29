using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private PolygonCollider2D collider; //AttackArea의 collider;
    private float attackPower; //공격 세기
    private BossHealth bossHealth; //BossHealth 참조
    private PhaseItem phaseItem; // PhaseItem 스크립트 참조

    private Hacking hacking;
    
    void Awake()
    {
        //공격 범위 콜라이더 할당
        collider = GetComponent<PolygonCollider2D>();
        hacking = GetComponentInParent<Hacking>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //공격 범위와 트리거 충돌 주체의 태그가 Boss 이면
        if (other.CompareTag("Boss"))
        {
            Debug.Log("[AttackArea] : 충돌");

            //상대의 Enemy 스크립트 참조
            bossHealth = other.GetComponent<BossHealth>();
            
            if (bossHealth != null)
            {
            //상대를 공격하기
            bossHealth.Damage(attackPower);
            }
            else
            {
                Debug.LogError("[AttackArea] : BossHealth 컴포넌트를 찾을 수 없습니다.");
            }

            Debug.Log("[AttackArea] :  Before calling onBossHealthDecrease");
            
            // Hacking 스크립트의 onBossHealthDecrease 호출
            hacking.onBossHealthDecrease(attackPower);
        }

        //공격 범위와 트리거 충돌 주체의 태그가 BossPhaseItem 이면

        else if (other.CompareTag("BossPhaseItem"))
        {
            Debug.Log("[AttackArea] : 충돌");

            //PhaseItem 스크립트 참조
            phaseItem = other.GetComponent<PhaseItem>();
            
            if (phaseItem.phase_item_hp != null)
            {
                //상대를 공격하기
                phaseItem.phaseItemDamage(10);
                
                if (phaseItem.phase_item_hp <= 0)
                {
                    // Hacking 스크립트의 onBossHealthDecrease 호출
                    hacking.onBossHealthDecrease(10);
                }

            }
            else
            {
                Debug.LogError("[AttackArea] : PhaseItem 컴포넌트를 찾을 수 없습니다.");
            }
            

        }
        else
        {
            Debug.Log("[AttackArea] : 충돌 주체가 적이 아님");
        }
    }

    //공격 세기 설정 함수
    public void SetAttackPower(float power)
    {
        Debug.Log("[AttackArea] : 공격 세기를 " + power + "로 설정");
        attackPower = power;
    }

    //플레이어 이동에 따라 공격범위 뒤집기
    public void Flip()
    {
        var points = collider.points;
        //플레이어가 오른쪽을 쳐다보고 있으면 collider offset의 x 값을 1, 아니면 -1로 설정
        for (int i = 0; i < points.Length; i++)
        {
            float prevX = points[i].x;
            points[i] = new Vector2(prevX * (-1f), points[i].y);
        }
        collider.points = points;
    }

    public void Flip(bool value)
    {
        var points = collider.points;
        float direction = value ? 1f : (-1f);
        //플레이어가 오른쪽을 쳐다보고 있으면 collider offset의 x 값을 1, 아니면 -1로 설정
        for (int i = 0; i < points.Length; i++)
        {
            float prevX = points[i].x;
            points[i] = new Vector2(prevX * direction, points[i].y);
        }
        collider.points = points;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//피격 패턴에서 사용되는 피격 오브젝트
public class Hittable : MonoBehaviour
{
    //오브젝트의 체력
    [SerializeField] private float health;
    public float Health { get; set; }
    
    //감소시킬 플레이어의 해킹 포인트 수치
    [Tooltip("감소시킬 해킹 포인트 수치(단위 : %)")]
    [SerializeField] private float hackPoint;
    public float HackPoint { get; private set; }
    
    //플레이어에게 공격 받을 경우의 행위
    public virtual void OnHit(float damage)
    {
        //공격세기 만큼 체력을 감소하기
        health -= damage;

        //체력이 0이하라면 파괴함수 호출
        if (health <= 0)
        {
            OnRemoved();
        }
    }
    
    //플레이어에게 공격을 받아 파괴되는 경우
    public virtual void OnRemoved(){}
    
    //플레이어에게 타격을 주는 행위
    public virtual void AttackPlayer(){}
    
    //오브젝트가 단계 별로 발전(변화)하는 행위
    public virtual void OnChange(){}
    
    //오브젝트가 발전(변화)를 완료하는 행위
    public virtual void OnComplete(){}
    
    //플레이어의 공격 범위와 충돌할 경우
    void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어의 공격범위와 충돌할 경우
        if (other.CompareTag("AttackArea"))
        {
            AttackArea attackArea = other.GetComponent<AttackArea>();
            OnHit(attackArea.GetAttackPower());
        }
    }
}

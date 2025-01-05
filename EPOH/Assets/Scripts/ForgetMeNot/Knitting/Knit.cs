using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class Knit : Hittable
{
    [SerializeField] private int curStep = 1;
    public int CurStep { get; set; }
    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void OnHit(float damage)
    {
        //공격세기 만큼 체력을 감소하기
        Health -= damage;
        Debug.Log($"[Knit] : 현재 단계({CurStep}), 체력 -{damage}({Health})");
        //체력이 0이하라면 파괴함수 호출
        if (Health <= 0)
        {
            //해킹 포인트 증가
            
            //애니메이션 파괴 트리거 발동
            _animator.SetTrigger("Destroy");
        }
    }

    public override void OnComplete()
    {
        base.OnComplete();
        //해킹 포인트 감소
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
    }
}

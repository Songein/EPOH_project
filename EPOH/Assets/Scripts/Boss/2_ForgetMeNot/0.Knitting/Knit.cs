using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class Knit : Hittable
{
    [SerializeField] private int _curStep = 1;

    public int CurStep
    {
        get { return _curStep; }
        set { _curStep = value; }
    }

    [SerializeField] private float _increasingHackPoint;

    public float IncreasingHackPoint
    {
        get { return _increasingHackPoint;}
        set { _increasingHackPoint = value; }
    }
    private Animator _animator;

    public void Awake()
    {
        CurStep = _curStep;
        IncreasingHackPoint = _increasingHackPoint;
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
            Debug.Log($"[Knit] : 한 단계 파괴 성공. 해킹 포인트 {IncreasingHackPoint}% 증가");
            BossManagerNew.Current.OnIncreaseHackingPoint?.Invoke(IncreasingHackPoint);
            //애니메이션 파괴 트리거 발동
            _animator.SetTrigger("Destroy");
        }
    }

    public override void OnComplete()
    {
        base.OnComplete();
        Debug.Log($"[Knit] : 5단계 완성. 해킹 포인트 {HackPoint}% 감소");
        BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(HackPoint);
        //해킹 포인트 감소
        FindObjectOfType<KnittingSkill>().CompleteKnit();
        Destroy(gameObject);
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
        Debug.Log($"[Knit] : 최종 파괴 성공.");
        FindObjectOfType<KnittingSkill>().CompleteKnit();
        Destroy(gameObject);
    }
}

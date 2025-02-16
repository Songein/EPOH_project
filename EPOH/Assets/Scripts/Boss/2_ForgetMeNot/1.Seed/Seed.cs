using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Hittable
{
    [SerializeField] private int _curStep = 1;

    public int CurStep
    {
        get { return _curStep; }
        set { _curStep = value; }
    }
    private Animator _animator;

    public void Awake()
    {
        CurStep = _curStep;
        _animator = GetComponent<Animator>();
    }

    public override void OnHit(float damage)
    {
        //공격세기 만큼 체력을 감소하기
        Health -= damage;
        Debug.Log($"[Seed] : 현재 단계({CurStep}), 체력 -{damage}({Health})");
        //체력이 0이하라면 파괴함수 호출
        if (Health <= 0)
        {
            Debug.Log($"[Seed] : 한 단계 파괴 성공.");
            //애니메이션 파괴 트리거 발동
            _animator.SetTrigger("Destroy");
        }
    }

    public override void OnComplete()
    {
        base.OnComplete();
        Debug.Log($"[Seed] : 4단계 완성. 해킹 포인트 {HackPoint}% 감소");
        BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(HackPoint);
        FindObjectOfType<SeedSkill>().CompleteSeed();
        //해킹 포인트 감소
        Destroy(gameObject);
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
        Debug.Log($"[Seed] : 최종 파괴 성공.");
        FindObjectOfType<SeedSkill>().CompleteSeed();
        Destroy(gameObject);
    }
}

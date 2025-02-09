using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;
public class Picture : Hittable
{
    [SerializeField] private float _decreaseDuration;
    
    void Start()
    {
        StartCoroutine(DecreaseHackPoint());
    }
    
    public override void OnRemoved()
    {
        base.OnRemoved();
        Debug.Log($"#[Picture] : 최종 파괴 성공.");
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public override void OnChange()
    {
        base.OnChange();
        //해킹 포인트 감소
        Debug.Log($"#[Picture] : {_decreaseDuration}초가 지나 해킹포인트 {HackPoint}% 만큼 감소");
        BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(HackPoint);
        if (Health > 0)
        {
            StartCoroutine(DecreaseHackPoint());
        }
    }

    IEnumerator DecreaseHackPoint()
    {
        yield return new WaitForSeconds(_decreaseDuration);
        OnChange();
    }
}

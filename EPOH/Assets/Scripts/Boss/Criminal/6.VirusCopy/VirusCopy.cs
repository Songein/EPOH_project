using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCopy : Attackable
{
    private BoxCollider2D _collider;
    [SerializeField] private bool _isFirstVirus;
    private void OnEnable()
    {
        if (_isFirstVirus)
        {
            StartCoroutine(FirstVirusAct());
            return;
        }
        // 콜라이더 비활성화
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        // 증식하고 특정 시간 뒤에 폭발
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(_duration);
        // 폭발 애니메이션 재생(우선 시각화를 위해 색 변경)
        GetComponent<SpriteRenderer>().color = Color.red;
        // 콜라이더 활성화
        _collider.enabled = true;
        
        //애니메이션이 없어서 임의로 파괴 코드 추가
        yield return new WaitForSeconds(2f);
        DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    IEnumerator FirstVirusAct()
    {
        yield return new WaitForSeconds(10f);
        DestroyObject();
    }
}

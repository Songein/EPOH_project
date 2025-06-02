using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal : Interactable
{
    public bool canInteract = false;
    [SerializeField] private bool _isRealCriminal;
    [SerializeField] private float _increaseHackPoint;
    [SerializeField] private float _decreaseHackPoint;
    [SerializeField] private float _explosionDamage;
    private Animator animator;
    private void Awake()
    {
        canInteract = false;
         animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        // 현재 상호작용 할 수 없다면(아직 섞기 및 이동 중이라면) 리턴
        if(!canInteract) return;
        
        // 상호작용 할 수 있다면
        if (_isRealCriminal)
        {
            // 1. 범죄자 깜짝 놀라는 애니메이션 실행
            animator.SetTrigger("Notice");
            // 2. 해킹 포인트 증가
            BossManagerNew.Current.OnIncreaseHackingPoint?.Invoke(_increaseHackPoint);
            
            // 모든 범죄자 사라짐
            DestroyAllCriminal();
        }
        else
        {
            // 가짜 범죄자이면 폭발 & 데미지 입고 해킹포인트 감소
            // 1. 폭발 애니메이션 실행
            if (!_isRealCriminal) {
                animator.SetTrigger("Explosion");
            }

            // 2. 데미지 입고 해킹포인트 감소
            _player.GetComponent<PlayerHealth>().Damage(_explosionDamage);
            BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(_decreaseHackPoint);
            
            // 진짜 범죄자 먼저 사라짐
            DestroyOnlyReal();
            // 그 후 나머지 범죄자 사라짐
           Invoke("DestroyAllCriminal",2f);
        }
    }

    IEnumerator DestroyCriminal() {
        yield return new WaitForSeconds(1f);
        Debug.Log("DestroyCriminal");
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }


    IEnumerator DestroyRealCriminal()
    {
        if (_isRealCriminal)
        {
            animator.SetTrigger("Laugh");
            yield return new WaitForSeconds(1f); //2초 후 사라짐 1초 + destroyCriminal 1초;
            Debug.Log("OnlyRealDie");
            StartCoroutine(DestroyCriminal());
        }
    }

    private void DestroyAllCriminal()
    {
        Criminal[] foundObjects = FindObjectsOfType<Criminal>();

        foreach (var criminal in foundObjects)
        {
            if (criminal == null) return;
            criminal.StartCoroutine(criminal.DestroyCriminal());
        }
    }

    private void DestroyOnlyReal()
    {
        Criminal[] foundObjects = FindObjectsOfType<Criminal>();

        foreach (var criminal in foundObjects)
        {
            if (criminal == null) return;

            criminal.StartCoroutine(criminal.DestroyRealCriminal());
        }
    }

    /*
    private void DestroyCriminal()
    {
        // 사라지는 애니메이션 실행
        // 씬 내에서 파괴
        Debug.Log("DestroyCriminal");
        gameObject.SetActive(false);
    }
    

    public void DestroyRealCriminal()
    {
        if (_isRealCriminal)
        {
            DestroyCriminal();
        }
    }

    private void DestroyAllCriminal()
    {
        Criminal[] foundObjects = FindObjectsOfType<Criminal>();

        foreach (var criminal in foundObjects)
        {
            if(criminal == null) return;
            criminal.DestroyCriminal();
        }
    }

    private void DestroyOnlyReal()
    {
        Criminal[] foundObjects = FindObjectsOfType<Criminal>();

        foreach (var criminal in foundObjects)
        {
            if(criminal == null) return;
            criminal.DestroyRealCriminal();
        }
    }
    */
}

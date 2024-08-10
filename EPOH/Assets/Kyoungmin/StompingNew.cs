using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompingNew : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;
    private SpriteRenderer sr;
    private bool facingRight;
    [SerializeField] private float facingTime = 0.5f; //플레이어를 바라보는 시간
    
    private int explosionCnt = 4; //폭발 이펙트 개수
    private int curCnt = 0; //현재 생성 개수
    private float explosionDistance; //폭발 이펙트 간 간격
    [SerializeField] private float maxScaleSize = 10f; //폭발 이펙트 최대크기
    [SerializeField] private float minScaleSize = 0f; //폭발 이펙트 최소크기
    [SerializeField] private float scaleSpeed = 0.01f; //폭발 이펙트 스케일 스피드
    [SerializeField] private int scaleParam = 20; //스케일 속도
    private float middleStandard; //폭발 이펙트 중간 지점 기준
    private bool nextExplosion = false; //다음 폭발 이펙트 플래그
    private Vector2 nextExplosionPos; //다음 폭발 이펙트 생성 위치
    private List<GameObject> explosionList = new List<GameObject>(); //폭발 이펙트 리스트
    [SerializeField] private GameObject explosionPrefab; //폭발 프리팹

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        explosionDistance = transform.GetComponent<CapsuleCollider2D>().size.x * 2;
    }

    public void Stomping1()
    {
        //개 그림자가 플레이어 위치에 생성
        playerPos = player.transform.position;
        transform.position = playerPos;
        //플레이어를 바라본다
        StartCoroutine(FacingPlayer());
        //폭발 이펙트 생성
        
    }

    
    IEnumerator FacingPlayer()
    {
        yield return new WaitForSeconds(facingTime);
        //facingTime 이후 플레이어가 위치한 방향을 바라본다
        Vector2 curPlayerPos = player.transform.position;
        if (curPlayerPos.x - transform.position.x >= 0)
        {
            facingRight = true;
            sr.flipX = false;
        }
        else
        {
            facingRight = false;
            sr.flipX = true;
        }
        
        //폭발 이펙트 생성하기
        nextExplosionPos = new Vector2(transform.position.x +  (facingRight ? 1f : -1f), transform.position.y);
        explosionList.Add(Instantiate(explosionPrefab, nextExplosionPos, Quaternion.identity));
        curCnt++;
    }

    public void GenerateExplosion()
    {
        if (curCnt > 4)
        {
            return;
        }
        nextExplosionPos = new Vector2(nextExplosionPos.x + explosionDistance * (facingRight ? 1f : -1f) * 1.5f, nextExplosionPos.y);
        explosionList.Add(Instantiate(explosionPrefab, nextExplosionPos, Quaternion.identity));
    }

    public IEnumerator DestroyExplosion()
    {
        if (curCnt > 4)
        {
            yield return new WaitForSeconds(0.2f);
            curCnt = 0;
            for (int i = 0; i < explosionList.Count; i++)
            {
                Destroy(explosionList[i]);
            } 
        }
        else
        {
            curCnt++;
        }
    }
    
}


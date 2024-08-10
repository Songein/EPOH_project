using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private Vector2 nextExplosionPos; //다음 폭발 이펙트 생성 위치
    private List<GameObject> explosionList = new List<GameObject>(); //폭발 이펙트 리스트
    [SerializeField] private GameObject explosionPrefab; //폭발 프리팹

    [SerializeField] private float stomping2Time = 2f; //발구르기 강화 전조 시간
    [SerializeField] private int rockCnt = 10; //돌 개수
    [SerializeField] private int rockSet = 4; //돌 세트 수
    private int curSet = 0; //현재 세트 수
    [SerializeField] private float rockFallPower; //돌 낙하 힘
    [SerializeField] private float rockSetDuration = 1.5f; //세트 간 간격
    [SerializeField] private Vector3 rockStartSpawn; //돌 스폰 위치 기준의 시작
    [SerializeField] private Vector3 rockEndSpawn; //돌 스폰 위치 기준의 끝
    [SerializeField] private GameObject rockPrefab; //돌 프리팹
    private List<GameObject> rockList = new List<GameObject>(); //돌 리스트
    private GameObject ground; //땅 오브젝트

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindWithTag("Ground");
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
        //폭발 이펙트 생성하기
        nextExplosionPos = new Vector2(transform.position.x +  (facingRight ? 1f : -1f), transform.position.y);
        explosionList.Add(Instantiate(explosionPrefab, nextExplosionPos, Quaternion.identity));
        curCnt++;
    }

    public void Stomping2()
    {
        //개 그림자 등장
        //두 발로 서는 애니메이
        Debug.Log("두 발로 선다.");
        //2초 후 땅을 내리친다. 땅 전체에 붉은색 전조(땅에 딛고 있으면 피해를 받음)
        StartCoroutine(GroundStomping());
        //동시에 하늘에서 돌(10개)가 떨어진다.(땅에 닿기까지 약 2초)
        
        //1.5초 후 다음 돌 세트가 떨어진다. X 4 세트

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

    IEnumerator GroundStomping()
    {
        //2초 후 땅을 내리치는 애니메이션으로 변경
        yield return new WaitForSeconds(stomping2Time);
        //애니메이션 변경 코드
        ground.GetComponent<GroundController>().ActiveAttack();
        StartCoroutine(GenerateRock());
        yield return new WaitForSeconds(1f); //여기 필요??
        ground.GetComponent<GroundController>().DisactiveAttack();
    }

    IEnumerator GenerateRock()
    {
        if (curSet >= 4)
        {
            yield break;
        }
        for (int i = 0; i < rockCnt; i++)
        {
            float randX = Random.Range(rockStartSpawn.x, rockEndSpawn.x);
            float randY = Random.Range(rockStartSpawn.y, rockEndSpawn.y);
            rockList.Add(Instantiate(rockPrefab, new Vector2(randX,randY),Quaternion.identity));
        }
        curSet++;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(GenerateRock());
    }
    
}


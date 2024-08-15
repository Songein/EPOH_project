using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StompingNew : MonoBehaviour
{
    private GameObject player; //플레이어 오브젝트
    private GameObject ground; //땅 오브젝트
    private List<GameObject> bossList = new List<GameObject>(); //보스 오브젝트 리스트
    [SerializeField] GameObject bossPrefab; //보스 프리팹
    private SpriteRenderer playerSR; //플레이어 스프라이트랜더러
    
    //발구르기 일반
    private bool facingRight; //오른쪽을 바라보고 있는지
    [SerializeField] private float facingTime = 0.5f; //플레이어를 바라보는 시간
    [SerializeField] private float precursorTime = 2f; //발구르기 일반 공격 전조 시간
    
    
    //발구르기 강화
    [SerializeField] private Vector3 spawnMiddlePoint; //스폰 중앙 위치
    [SerializeField] private float stomping2Time = 2f; //발구르기 강화 전조 시간
    [SerializeField] private int rockCnt = 10; //돌 개수
    [SerializeField] private int rockSet = 4; //돌 세트 수
    private int curSet = 0; //현재 세트 수
    [SerializeField] private float rockFallPower; //돌 낙하 힘
    [SerializeField] private float rockSetDuration = 1.5f; //세트 간 간격
    [SerializeField] private Vector3 rockStartSpawn; //돌 스폰 위치 기준의 시작
    [SerializeField] private Vector3 rockMiddleSpawn; //돌 스폰 위치 기준의 중간
    [SerializeField] private Vector3 rockEndSpawn; //돌 스폰 위치 기준의 끝
    [SerializeField] private GameObject rockPrefab; //돌 프리팹
    private List<GameObject> rockList = new List<GameObject>(); //돌 리스트
    [SerializeField] private GameObject warning; //전조현상 주의 오브젝트
    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ground = GameObject.FindWithTag("Ground");
        playerSR = GetComponent<SpriteRenderer>();
    }

    public void Stomping1()
    {
        //보스 리스트 초기화
        bossList.Clear();
        //개 그림자가 플레이어 위치에 생성
        bossList.Add(Instantiate(bossPrefab, player.transform.position, quaternion.identity));
        //플레이어를 바라본다
        StartCoroutine(FacingPlayer());
        //폭발 이펙트 생성하기
        
    }

    public void Stomping2()
    {
        //boss list init
        bossList.Clear();
        //개 그림자 등장
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            bossList.Add(Instantiate(bossPrefab, spawnMiddlePoint, quaternion.identity));
            bossList[0].GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            bossList.Add(Instantiate(bossPrefab, spawnMiddlePoint, quaternion.identity));
            bossList[0].GetComponent<SpriteRenderer>().flipX = false;
        }
        //두 발로 서는 애니메이션
        Debug.Log("두 발로 선다.");
        ground.GetComponent<SpriteRenderer>().color = Color.red;
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
        //stomping wave 오브젝트 활성화
        GameObject stompingWave = bossList[0].transform.GetChild(2).gameObject;
        yield return new WaitForSeconds(precursorTime);
        //플레이어 위치에 따라 보스가 바라보는 방향 변경 및 왼/오 wave 활성화
        stompingWave.SetActive(true);
        if (curPlayerPos.x - transform.position.x >= 0)
        {
            //오른쪽 바라보고 있음 -> 오른쪽 wave 활성화
            facingRight = true;
            playerSR.flipX = false;
            stompingWave.GetComponent<Animator>().Play("Right Explosion");
        }
        else
        {
            //왼쪽 바라보고 있음 -> 왼쪽 wave 활성화
            facingRight = false;
            playerSR.flipX = true;
            stompingWave.GetComponent<Animator>().Play("Left Explosion");
        }
    }
    
    IEnumerator GroundStomping()
    {
        warning.SetActive(true);
        rockList.Clear();
        curSet = 0;
        //2초 후 땅을 내리치는 애니메이션으로 변경
        yield return new WaitForSeconds(stomping2Time);
        warning.SetActive(false);
        //애니메이션 변경 코드
        ground.GetComponent<GroundController>().ActiveAttack();
        StartCoroutine(GenerateRock());
        yield return new WaitForSeconds(0.5f); //여기 필요??
        ground.GetComponent<GroundController>().DisactiveAttack();
    }

    IEnumerator GenerateRock()
    {
        if (curSet >= 4)
        {
            Destroy(bossList[0]);
            yield break;
        }
        for (int i = 0; i < rockCnt/2; i++)
        {
            float randX = Random.Range(rockStartSpawn.x, rockMiddleSpawn.x);
            float randY = Random.Range(rockStartSpawn.y, rockMiddleSpawn.y);
            GameObject rock = Instantiate(rockPrefab, new Vector2(randX, randY), Quaternion.identity);
            float randGravity = Random.Range(1, 3);
            rock.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            rockList.Add(rock);
        }
        for (int i = 0; i < rockCnt/2; i++)
        {
            float randX = Random.Range(rockMiddleSpawn.x, rockEndSpawn.x);
            float randY = Random.Range(rockMiddleSpawn.y, rockEndSpawn.y);
            GameObject rock = Instantiate(rockPrefab, new Vector2(randX, randY), Quaternion.identity);
            float randGravity = Random.Range(1, 3);
            rock.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            rockList.Add(rock);
        }
        curSet++;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(GenerateRock());
    }
    
}


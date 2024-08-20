using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomping2 : MonoBehaviour, BossSkillInterface
{
    //BossDogController 참조
    private BossDogController _dog;
    
    //땅 오브젝트
    private GameObject ground; 
  
    //발구르기 강화
    [SerializeField] private float stompingPrecursorTime = 2f; //발구르기 강화 전조 시간
    [SerializeField] private int rockCnt = 12; //돌 개수
    [SerializeField] private int rockSet = 4; //돌 세트 수
    [SerializeField] private float rockFallPower; //돌 낙하 힘
    [SerializeField] private float rockSetDuration = 1.5f; //세트 간 간격
    
    [SerializeField] private Vector3 rockStartSpawn; //돌 스폰 위치 기준의 시작
    [SerializeField] private Vector3 rockEndSpawn; //돌 스폰 위치 기준의 끝
    private float rockMiddle1SpawnX; //돌 스폰 위치 기준의 중간1
    private float rockMiddle2SpawnX; //돌 스폰 위치 기준의 중간2
  
    
    [SerializeField] private GameObject rockPrefab; //돌 프리팹
    [SerializeField] private GameObject warning; //전조현상 주의 오브젝트
    
    private int curSet = 0; //현재 세트 수
    private List<GameObject> rockList = new List<GameObject>(); //돌 리스트
    private int _random; //spawn 랜덤 난수
    
    
    private void Awake()
    {
        _dog = GetComponent<BossDogController>();
        ground = GameObject.FindWithTag("Ground");
        float distance = (rockEndSpawn.x - rockStartSpawn.x) / 3;
        rockMiddle1SpawnX = rockStartSpawn.x + distance;
        rockMiddle2SpawnX = rockStartSpawn.x + distance * 2;
    }

    public void Activate()
    {
        StartCoroutine(StartStomping2());
    }
    
    IEnumerator StartStomping2()
    {
        //boss 리스트 초기화
        _dog.bossList.Clear();
        //돌 관련 변수 초기화
        rockList.Clear();
        curSet = 0;
        
        //개 그림자 등장
        _random = Random.Range(0, 2);
        if (_random == 0)
        {
            _dog.GenerateDog(_dog.spawnMiddlePoint);
        }
        else
        {
            _dog.GenerateDog(_dog.spawnMiddlePoint);
        }
        
        //두 발로 서는 애니메이션
        Debug.Log("두 발로 선다.");
        //주의 표시 활성화
        warning.SetActive(true);
        
        //2초 후 땅을 내리친다. 땅 전체에 붉은색 전조(땅에 딛고 있으면 피해를 받음)
        yield return new WaitForSeconds(stompingPrecursorTime);
        warning.SetActive(false);
        ground.GetComponent<GroundController>().ActiveAttack();
        
        //돌 공격 시작
        StartCoroutine(GenerateRock());
        
        //땅 공격 해제
        yield return new WaitForSeconds(0.5f);
        ground.GetComponent<GroundController>().DisactiveAttack();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator GenerateRock()
    {
        if (curSet >= rockSet)
        {
            Destroy(_dog.bossList[0]);
            yield break;
        }
        for (int i = 0; i < rockCnt/3; i++)
        {
            float randX = Random.Range(rockStartSpawn.x, rockMiddle1SpawnX);
            float randY = Random.Range(rockStartSpawn.y, rockEndSpawn.y);
            GameObject rock = Instantiate(rockPrefab, new Vector2(randX, randY), Quaternion.identity);
            float randGravity = Random.Range(1, 3);
            rock.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            rockList.Add(rock);
        }
        for (int i = 0; i < rockCnt/3; i++)
        {
            float randX = Random.Range(rockMiddle1SpawnX, rockMiddle2SpawnX);
            float randY = Random.Range(rockStartSpawn.y, rockEndSpawn.y);
            GameObject rock = Instantiate(rockPrefab, new Vector2(randX, randY), Quaternion.identity);
            float randGravity = Random.Range(1, 3);
            rock.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            rockList.Add(rock);
        }
        for (int i = 0; i < rockCnt/3; i++)
        {
            float randX = Random.Range(rockMiddle2SpawnX, rockEndSpawn.x);
            float randY = Random.Range(rockStartSpawn.y, rockEndSpawn.y);
            GameObject rock = Instantiate(rockPrefab, new Vector2(randX, randY), Quaternion.identity);
            float randGravity = Random.Range(1, 3);
            rock.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            rockList.Add(rock);
        }
        curSet++;
        yield return new WaitForSeconds(rockSetDuration);
        StartCoroutine(GenerateRock());
    }
}

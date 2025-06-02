using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Howling2 : MonoBehaviour, BossSkillInterface
{
    //하울링 강화
    [SerializeField] private float absorbingDuration = 5f; //플레이어를 빨아들이는 스킬 지속시간
    [SerializeField] private float absorbPower; //빨아들이는 힘
    public bool absorbFlag = false; //빨아드리는 스킬 플래그
    //충격파 프리팹
    public GameObject shockWavePrefab;

    [SerializeField] private GameObject _dogPrefab;
    //보스 스폰 랜덤 난수
    private int _random;
    //보스 흡수 방향
    private Vector2 _direction;
    //보스 흡수 최종 연산 힘
    private float _power;
    // 플레이어
    private PlayerController _player;
    // 스폰된 보스
    private GameObject _dog;
    //보스 정보
    private BossData _bossData;

    void Start()
    {
        _bossData = BossManagerNew.Current.bossData;
        _player = BossManagerNew.Current.player;
    }
    
    void FixedUpdate()
    {
        if (absorbFlag)
        {
            //보스 개와 플레이어 간 거리 계산
            float distance = Mathf.Abs(_player.transform.position.x - _dog.transform.position.x);
            //거리의 제곱근 값 * 10f -> 소수점 두자리 내림
            float v = Mathf.Floor(Mathf.Sqrt(distance) * 10f) / 100f;
            //Debug.Log($"Distance: {distance}, V: {v}, Direction: {_direction}, Force: {absorbPower * (1-v)}");
            
            //최종 흡수 힘 = 흡수 힘 * (1-v)
            _power = absorbPower * (1 - v);
            Debug.Log(_power);
            //_direction 방향으로 _power 만큼의 힘을 가하기
            _player.GetComponent<Rigidbody2D>().AddForce(_direction * _power,ForceMode2D.Force);
        }
    }
    public void Activate()
    {
        //양 쪽 맵 중 한 곳에서 랜덤으로 등장
        _random = Random.Range(0, 2);
        if (_random == 0)
        {
            //왼쪽
            Vector2 leftPoint = new Vector2(_bossData._leftBottom.x, _bossData._leftBottom.y);
            SpawnDog(leftPoint);
        }
        else
        {
            //오른쪽
            Vector2 rightPoint = new Vector2(_bossData._rightTop.x, _bossData._leftBottom.y);
            SpawnDog(rightPoint);
        }
    }
    //보스 개의 흡수 방향 감지
    IEnumerator TurnAround()
    {
        //흡수 끝나면 코루틴 끝
        if (!absorbFlag)
        {
            yield break;
        }
        //플레이어 위치 방향에 따라 흡수 방향인 _direction 값 설정
        if (BossManagerNew.Current.IsPlayerRight(_dog.transform))
        {
            _direction = Vector2.left;
        }
        else
        {
            _direction = Vector2.right;
        }
        //플레이어 위치에 따라 바로 방향 바뀌기 보다는 시간 차를 두고 자연스럽게 변경
        yield return new WaitForSeconds(2f);
        StartCoroutine(TurnAround());
    }
    
    void SpawnDog(Vector2 spawnPoint)
    {
        _dog = null;
        GameObject boss = Instantiate(_dogPrefab, spawnPoint, Quaternion.identity);
        _dog = boss;
        BossManagerNew.Current.IsPlayerRight(boss.transform);
        Animator animator = boss.GetComponent<Animator>();
        animator.SetTrigger("Howling2");
        
        //플레이어 첫 방향 설정
        if (BossManagerNew.Current.IsPlayerRight(_dog.transform))
        {
            _player.Flip(false);
        }
        else
        {
            _player.Flip(true);
        }
        //플레이어에게 개 방향으로 빨려들어가는 힘 작용(개와 가까워질수록 빨려들어가는 힘 강화)
        absorbFlag = true;
        //보스 개의 방향 감지 코루틴 실행
        StartCoroutine(TurnAround());
    }
}

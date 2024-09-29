using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Howling1 : MonoBehaviour, BossSkillInterface
{
    //충격파 발생까지의 시간
    [SerializeField] private float shockWaveStartTime = 2f;
    //충격파 프리팹
    [SerializeField] private GameObject shockWavePrefab;
    //BossDogController 참조
    private BossDogController _dog;
    //보스 개 스폰 난수
    private int _random;
    
    void Start()
    {
        _dog = GetComponent<BossDogController>();
    }
    
    public void Activate()
    {
        StartCoroutine(StartHowling1());
    }

    //일반 하울링 스킬 함수
    public IEnumerator StartHowling1()
    {
        //보스 리스트 초기화
        _dog.bossList.Clear();
        
        //중앙(1체), 맵 양 끝(2체) 중 등장할 곳 결정
        _random = Random.Range(0, 2);
        if (_random == 0)
        {
            //중앙에 보스 생성
            _dog.GenerateDog(_dog.spawnMiddlePoint);
        }
        else if (_random == 1)
        {
            //맵 양 끝에 보스 생성
            _dog.GenerateDog(_dog.spawnLeftPoint);
            _dog.GenerateDog(_dog.spawnRightPoint);
        }
        
        //고개를 드는 애니메이션으로 전환
        Debug.Log("개 그림자 고개를 든다.");
        
        //2초 후에 충격파 발생
        yield return new WaitForSeconds(shockWaveStartTime);
        
        //충격파 전조 시간이 지나고충격파 발생
        foreach (var boss in _dog.bossList)
        {
            Instantiate(shockWavePrefab, boss.transform.position, Quaternion.identity);
        }
    }
}

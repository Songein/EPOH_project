using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Howling1 : MonoBehaviour, BossSkillInterface
{
    //충격파 프리팹
    public GameObject shockWavePrefab;
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

        foreach (var dog in _dog.bossList)
        {
            Animator animator = dog.GetComponent<Animator>();
            animator.SetTrigger("Howling1");
        }
    }
}

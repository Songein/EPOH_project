using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Howling1 : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject dogPrefab; //하울링 보스 개 프리팹
    public  GameObject shockWavePrefab;  //충격파 프리팹
    
    //보스 개 스폰 난수
    private int _random;
    //보스 정보
    private BossData _bossData;

    void Start()
    {
        _bossData = BossManagerNew.Current.bossData;
    }
    
    public void Activate()
    {        
        //중앙(1체), 맵 양 끝(2체) 중 등장할 곳 결정
        _random = Random.Range(0, 2);
        if (_random == 0)
        {
            //중앙에 보스 생성
            Vector2 middlePoint = new Vector2(0, _bossData._leftBottom.y);
            SpawnDog(middlePoint);
        }
        else if (_random == 1)
        {
            //맵 양 끝에 보스 생성
            Vector2 leftPoint = new Vector2(_bossData._leftBottom.x, _bossData._leftBottom.y);
            Vector2 rightPoint = new Vector2(_bossData._rightTop.x, _bossData._leftBottom.y);
            SpawnDog(leftPoint);
            SpawnDog(rightPoint);
        }
    }

    void SpawnDog(Vector2 spawnPoint)
    {
        GameObject boss = Instantiate(dogPrefab, spawnPoint, Quaternion.identity);
        BossManagerNew.Current.SetBossFlip(boss.transform);
        Animator animator = boss.GetComponent<Animator>();
        animator.SetTrigger("Howling1");
    }
}

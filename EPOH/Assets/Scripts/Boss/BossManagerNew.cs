using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerNew : MonoBehaviour
{
    public static BossManagerNew Current { get; private set; }
    public BossData bossData;
    public Action<float> OnDecreaseHackingPoint;
    public Action<float> OnIncreaseHackingPoint;
    
    private void Awake()
    {
        Current = this; // 현재 씬의 BossManager를 저장
    }

    public void StartBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 시작");
    }
    
    public void EndBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 종료");     
    }
}

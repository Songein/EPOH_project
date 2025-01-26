using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerNew : Singleton<BossManagerNew>
{
    public BossData bossData;
    public Action<float> OnDecreaseHackingPoint;
    public Action<float> OnIncreaseHackingPoint;

    public void StartBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 시작");   
    }
    
    public void EndBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 종료");     
    }
}

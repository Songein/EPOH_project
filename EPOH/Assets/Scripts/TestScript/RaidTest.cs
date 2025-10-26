using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RaidTest : MonoBehaviour
{
    public bool isFinal = false;
    public void Clear()
    {
        if (isFinal)
        {
            BossManagerNew.Current.ClearFinalBossRaidAsync().Forget();
            return;
        }
        BossManagerNew.Current.ClearBossRaidAsync().Forget();
    }

    public void Fail()
    {
        PlayerController.Instance.GetComponent<PlayerHealth>().Die();
    }
}

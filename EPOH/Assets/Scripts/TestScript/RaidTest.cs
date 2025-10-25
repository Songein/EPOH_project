using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RaidTest : MonoBehaviour
{
    public void Clear()
    {
        BossManagerNew.Current.ClearBossRaidAsync().Forget();
    }

    public void Fail()
    {
        PlayerController.Instance.GetComponent<PlayerHealth>().Die();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartEventTest : MonoBehaviour
{
    public void StartEvent(string eventID)
    {
        EventManager.Instance.ExecuteEvent(eventID).Forget();
    }
    
    public void ClearEvent(int bossNum)
    {
        GameManager.instance.bossClearInfo[bossNum] = true;
    }
}

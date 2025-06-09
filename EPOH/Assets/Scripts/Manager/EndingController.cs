using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    public void LeaveTheWork()
    {
        if (CheckBossObject())
        {
            // 보스 물건을 모두 챙긴 경우
            EventManager.Instance.ExecuteEvent("Event_060").Forget();
            SceneChanger.Instance.ChangeScene("NormalEnding").Forget();
        }
        else
        {
            // 보스 물건을 모두 챙기지 않은 경우
            EventManager.Instance.ExecuteEvent("Event_061").Forget();
            SceneChanger.Instance.ChangeScene("BadEnding").Forget();
        }
    }

    public bool CheckBossObject()
    {
        foreach (var bossObject in GameManager.instance.bossObjectAcquiredInfo)
        {
            if (!bossObject)
            {
                return false;
            }
        }

        return true;
    }
}

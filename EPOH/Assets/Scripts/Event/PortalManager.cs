using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    public PortalTeleportManager.PortalState curPortalState;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerInteract.Instance.canInteract)
        {
            if (IsAcceptState())
            {
                PlayerInteract.Instance.OnInteract = null;
                PlayerInteract.Instance.OnInteract += () =>
                {
                    StartCoroutine(PortalTeleportManager.Instance.StartOperatePortal(curPortalState));
                };
            }
            else
            {
                Debug.LogWarning("포탈로 이동할 수 없는 상태입니다.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerInteract.Instance.canInteract)
        {
            PlayerInteract.Instance.OnInteract = null;
        }
    }

    public bool IsAcceptState()
    {
        switch (curPortalState)
        {
            // 메인룸에서 오피스로 가기 위해서는 의뢰를 수락한 상태여야 함.
            case PortalTeleportManager.PortalState.MainToOffice:
                if (GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req1_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req2_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req3_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req4_Start) return true;
                return false;
            // 보스룸에서 오피스로 가기 위해서는 보스 레이드를 클리어한 상태여야 함.
            case PortalTeleportManager.PortalState.BossToOffice:
                if (GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req1_Clear
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req2_Clear
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req3_Clear
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req4_Clear) return true;
                return false;
            // 오피스에서 메인룸으로 가기 위해서는 의뢰 수락을 한 상태이거나, 보스 레이드 실패한 경우가 아니어야 함.
            case PortalTeleportManager.PortalState.OfficeToMain:
                if (GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req1_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req2_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req3_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req4_Start) return false;
                if (GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req1_Fail
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req2_Fail
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req3_Fail
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req4_Fail) return false;
                return true;
            // 오피스에서 보스룸으로 가기 위해서는 의뢰를 수락하여 준비 단계 상태여야 함.
            case PortalTeleportManager.PortalState.OfficeToBoss:
                if (GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req1_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req2_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req3_Start
                    || GameManager.instance.ProgressState == GameManager.ProgressId.Progress_Req4_Start) return true;
                return false;
            // 그 외의 상태에서는 포탈 이동에 제약이 없음.
            default:
                return true;
        }
    }
}

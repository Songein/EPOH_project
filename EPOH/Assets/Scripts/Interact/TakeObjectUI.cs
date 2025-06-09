using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObjectUI : UIBase
{
    [SerializeField] private int _bossObjectInfo;
    [SerializeField] private GameObject _bossObject;
    
    public override void OnOpen(EffectStructure effect)
    {
        base.OnOpen(effect);
        // 팝업 오브젝트 활성화
        transform.GetChild(0).gameObject.SetActive(true);
        // 현재 실행 중인 이벤트에 따라 오브젝트 할당
        switch (EventManager.Instance.currentEventID)
        {
            case "Event_056":
                _bossObjectInfo = 0;
                _bossObject = GameObject.Find("RobotDog");
                break;
            case "Event_057":
                _bossObjectInfo = 1;
                _bossObject = GameObject.Find("Diary");
                break;
            case "Event_058":
                _bossObjectInfo = 2;
                _bossObject = GameObject.Find("Picture");
                break;
            case "Event_059":
                _bossObjectInfo = 3;
                _bossObject = GameObject.Find("Chocolate");
                break;
        }
    }
    
    public override void OnClose()
    {
        base.OnClose();
        transform.GetChild(0).gameObject.SetActive(false);
        EffectManager.Instance.OnEffectEnd?.Invoke();
    }

    public void ClickYesBtn()
    {
        Debug.LogWarning($"보스 오브젝트{_bossObjectInfo} 획득");
        GameManager.instance.bossObjectAcquiredInfo[_bossObjectInfo] = true;
        Destroy(_bossObject);
        UIManager.Instance.CloseTopUI();
    }

    public void ClickNoBtn()
    {
        Debug.LogWarning($"보스 오브젝트{_bossObjectInfo} 미획득");
        GameManager.instance.bossObjectAcquiredInfo[_bossObjectInfo] = false;
        UIManager.Instance.CloseTopUI();
    }
}

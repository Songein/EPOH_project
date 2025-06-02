using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RequestUI : UIBase
{
    [SerializeField] private Image _requestImg;
    [Header("의뢰 수락 시 이벤트 정보")] public List<string> acceptEventId = new List<string>();

    public override void OnOpen(EffectStructure effect)
    {
        base.OnOpen(effect);
        // 팝업 이미지 갈아끼기
        _requestImg.sprite = UIManager.Instance.GetSpriteFromFilePath(effect.ArtResourcePath);
        // 팝업 오브젝트 활성화
        transform.GetChild(0).gameObject.SetActive(true);

        if (EventManager.Instance.currentEventID == "Event_007")
        {
            StartCoroutine(DialogueManager.Instance.StartDialogue("Dialogue_005"));
        }
    }
    
    public override void OnClose()
    {
        base.OnClose();
        transform.GetChild(0).gameObject.SetActive(false);
        EffectManager.Instance.OnEffectEnd?.Invoke();
    }

    public override void HandleMouseInput()
    {
        // Space 버튼 입력 처리
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.CloseTopUI();
        }
    }

    public void AcceptRequest()
    {
        switch (GameManager.instance.ProgressState)
        {
            case GameManager.ProgressId.Progress_Req1:
                EventManager.Instance.ExecuteEvent(acceptEventId[0]).Forget();
                break;
            case GameManager.ProgressId.Progress_Req2:
                EventManager.Instance.ExecuteEvent(acceptEventId[1]).Forget();
                break;
            case GameManager.ProgressId.Progress_Req3:
                EventManager.Instance.ExecuteEvent(acceptEventId[2]).Forget();
                break;
            case GameManager.ProgressId.Progress_Req4:
                EventManager.Instance.ExecuteEvent(acceptEventId[3]).Forget();
                break;
        }
        UIManager.Instance.CloseTopUI();
    }
}

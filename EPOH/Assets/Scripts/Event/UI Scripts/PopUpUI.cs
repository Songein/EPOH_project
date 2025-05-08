using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : UIBase
{
    [SerializeField] private Image _popUpSprite;
    public override void OnOpen(EffectStructure effect)
    {
        base.OnOpen(effect);
        // 팝업 이미지 갈아끼기
        _popUpSprite.sprite = UIManager.Instance.GetSpriteFromFilePath(effect.ArtResourcePath);
        // 팝업 오브젝트 활성화
        transform.GetChild(0).gameObject.SetActive(true);
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
}

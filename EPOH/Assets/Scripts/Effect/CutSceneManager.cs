using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : UIBase
{
    [SerializeField] private Image _cutSceneSprite;
    public override void OnOpen(EffectStructure effect)
    {
        base.OnOpen(effect);
        // 팝업 이미지 갈아끼기
        _cutSceneSprite.sprite = UIManager.Instance.GetSpriteFromFilePath(effect.ArtResourcePath);
        // 팝업 오브젝트 활성화
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
    public override void OnClose()
    {
        base.OnClose();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

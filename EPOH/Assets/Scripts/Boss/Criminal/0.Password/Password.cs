using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Password : Interactable
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _hackPoint = 3f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Sprite _changedSprite;
    private bool _isChanged = false;
    
    void OnEnable()
    {
        base.OnEnable();
        _sr = GetComponent<SpriteRenderer>();
        StartCoroutine(DecreaseHackPoint());
        _isChanged = false;
    }
    
    //플레이어가 상호작용 할 경우 실행될 코드
    public override void OnInteract()
    {
        base.OnInteract();
        //단어 이미지 변경
        _sr.sprite = _changedSprite;
        _isChanged = true;
    }

    IEnumerator DecreaseHackPoint()
    {
        yield return new WaitForSeconds(_duration);
        if (!_isChanged)
        {
            //바꾸는 것 실패 -> 데미지 및 해킹포인트 감소
            BossManagerNew.Instance.OnDecreaseHackingPoint?.Invoke(_hackPoint);
        }
        Destroy(gameObject);
    }
}

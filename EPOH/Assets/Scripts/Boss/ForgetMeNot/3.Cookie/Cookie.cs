using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;
public class Cookie : Interactable
{
    [SerializeField] private float _recoveryHealth;
    [SerializeField] private float _decreasingHackPoint;
    public override void OnInteract()
    {
        base.OnInteract();
        //체력 회복
        Debug.Log($"[Cookie] : 플레이어 체력 {_recoveryHealth}만큼 회복");
        //해킹포인트 감소
        Debug.Log($"[Cookie] : 플레이어 해킹포인트 {_decreasingHackPoint}% 만큼 감소");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;
public class Cookie : Interactable
{
    [SerializeField] private float _recoveryHealth;
    [SerializeField] private float _decreasingHackPoint;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    public override void OnInteract()
    {
        base.OnInteract();
        //체력 회복
        Debug.Log($"[Cookie] : 플레이어 체력 {_recoveryHealth}만큼 회복");
        playerHealth.IncreaseHealth(80);
        //해킹포인트 감소
        Debug.Log($"[Cookie] : 플레이어 해킹포인트 {_decreasingHackPoint}% 만큼 감소");
        BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(_decreasingHackPoint);
        BossManagerNew.Current.OnSkillEnd?.Invoke();
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _criminalPrefab;
    [SerializeField] private GameObject _jailPrefab;
    
    public void Activate()
    {
        // 범죄자를 중앙에 스폰
        Instantiate(_jailPrefab);
        // 중앙 top에 jail 스폰
        Instantiate(_criminalPrefab);
    }
}

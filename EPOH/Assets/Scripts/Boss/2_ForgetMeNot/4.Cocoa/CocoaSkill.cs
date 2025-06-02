using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoaSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _cocoaPrefab;
    public void Activate()
    {
        int _x = Random.Range(-11, 3);
        Instantiate(_cocoaPrefab, new Vector3(_x, 0, 0), Quaternion.identity);
    }
}

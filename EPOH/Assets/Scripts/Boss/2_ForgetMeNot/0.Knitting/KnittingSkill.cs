using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _knitPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    public void Activate()
    {
        //3개의 목도리 생성
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_knitPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}

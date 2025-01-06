using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private List<Transform> _seedSpawnPoints;

    public void Activate()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_seedPrefab, _seedSpawnPoints[i].position, Quaternion.identity);
        }
    }
}

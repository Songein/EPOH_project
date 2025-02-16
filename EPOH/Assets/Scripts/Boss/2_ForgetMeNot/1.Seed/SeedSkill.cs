using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private List<Transform> _seedSpawnPoints;
    [SerializeField] private int _seedCnt = 5;
    private int _seedCompleteCnt;
    private bool _isSkillEnd = false;

    private void Update()
    {
        if (!_isSkillEnd && _seedCompleteCnt == _seedCnt)
        {
            _isSkillEnd = true;
            BossManagerNew.Current.OnSkillEnd?.Invoke();
        }
    }
    public void Activate()
    {
        for (int i = 0; i < _seedCnt; i++)
        {
            Instantiate(_seedPrefab, _seedSpawnPoints[i].position, Quaternion.identity);
        }
    }
    
    public void CompleteSeed()
    {
        _seedCompleteCnt++;
    }
}

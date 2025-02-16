using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _knitPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _knitCnt = 3;
    [SerializeField] private int _knitCompleteCnt = 0;
    private bool _isSkillEnd = false;

    private void Update()
    {
        if (!_isSkillEnd && _knitCompleteCnt == _knitCnt)
        {
            _isSkillEnd = true;
            BossManagerNew.Current.OnSkillEnd?.Invoke();
        }
    }

    public void Activate()
    {
        _isSkillEnd = false;
        //3개의 목도리 생성
        for (int i = 0; i < _knitCnt; i++)
        {
            Instantiate(_knitPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }

    public void CompleteKnit()
    {
        _knitCompleteCnt++;
    }
    
}

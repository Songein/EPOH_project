//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private List<GameObject> _knitPrefab;
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
        List<int> randomnum = numbers();
        _isSkillEnd = false;
        //3개의 목도리 생성
        for (int i = 0; i < _knitCnt; i++)
        {
            Instantiate(_knitPrefab[randomnum[i]], _spawnPoints[i].position, Quaternion.identity);
        }
    }

    List<int> numbers() {
        List<int> shuffle = new List<int> { 0,1,2 };

        for (int i = 0; i < 3; i++) {
            int temp = Random.Range(i, 3);
            (shuffle[i] ,shuffle[temp]) = (shuffle[temp], shuffle[i]);
        }

        return shuffle;
    }

    public void CompleteKnit()
    {
        _knitCompleteCnt++;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _ballPrefab;
    private float _x;
    private float _y;
    public void Activate()
    {
        //공을 랜덤 위치에 생성하기
        BossData bossData = BossManagerNew.Instance.bossData;
        _x = Random.Range(bossData._leftBottom.x,bossData._rightTop.x);
        _y = Random.Range(bossData._leftBottom.y, bossData._rightTop.y);
        Vector3 ballPos = new Vector3(_x, _y, 0);

        Instantiate(_ballPrefab, ballPos, Quaternion.identity);
    }
}

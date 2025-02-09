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
        BossData bossData = BossManagerNew.Current.bossData;
        int randomValue = Random.Range(0, 2);
        Vector3 ballPos;
        if (randomValue == 1)
        {
            ballPos = new Vector3(bossData._leftBottom.x, bossData._rightTop.y, 0);
        }
        else
        {
            ballPos = new Vector3(bossData._rightTop.x, bossData._rightTop.y, 0);
        }

        

        Instantiate(_ballPrefab, ballPos, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollSkill : MonoBehaviour,BossSkillInterface
{
    [SerializeField] private GameObject _dollPrefab;
    public void Activate()
    {
        BossData bossData = BossManagerNew.Instance.bossData;
        //랜덤한 위치에 인형 생성
        float _x = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
        float _y = Random.Range(bossData._leftBottom.y, bossData._rightTop.y);

        Vector3 dollPos = new Vector3(_x, _y, 0);
        Instantiate(_dollPrefab, dollPos, Quaternion.identity);
    }
}

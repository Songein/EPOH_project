using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LullabySkill : MonoBehaviour,BossSkillInterface
{
    [SerializeField] private GameObject _lullabyPrefab;
    public void Activate()
    {
        //맵 상단에 Lullaby 나타내기
        BossData bossData = BossManagerNew.Current.bossData;
        float _x = (bossData._leftBottom.x + bossData._rightTop.x)/2.0f;
        float _y = bossData._rightTop.y;
        Vector3 randomPos = new Vector3(_x, _y -3f, 0);

        Instantiate(_lullabyPrefab, randomPos, Quaternion.identity);
    }
}

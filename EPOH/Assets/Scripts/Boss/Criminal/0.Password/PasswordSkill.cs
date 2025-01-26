using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PasswordSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _wordPrefab;
    public void Activate()
    {
        //맵 내의 랜덤한 위치에 단어 오브젝트 생성
        float _x = Random.Range(BossManagerNew.Instance.bossData._leftBottom.x,
            BossManagerNew.Instance.bossData._rightTop.x);
        float _y = Random.Range(BossManagerNew.Instance.bossData._leftBottom.y,
            BossManagerNew.Instance.bossData._rightTop.y);
        Vector2 newPos = new Vector2(_x, _y);
        Instantiate(_wordPrefab, newPos,quaternion.identity);
    }
}

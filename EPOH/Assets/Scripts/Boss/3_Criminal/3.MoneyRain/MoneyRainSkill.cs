using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRainSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private int _moneyCnt;
    [SerializeField] private int _rainCnt;
    [SerializeField] private float _moneySpeed; // 중력 일단 랜덤으로 설정했는데 필요한지 논의
    [SerializeField] private float _rainInterval;

    [SerializeField] private float _topSpawnRange; // top에서 부터 동전이 스폰되는 범위
    [SerializeField] private List<GameObject> _moneyList = new List<GameObject>();
    [SerializeField] private float _moneyMinGravity;
    [SerializeField] private float _moneyMaxGravity;

    public void Activate()
    {
        BossData bossData = BossManagerNew.Current.bossData;
        
        StartCoroutine(SpawnMoney(bossData));
    }

    IEnumerator SpawnMoney(BossData bossData)
    {
        for (int rain = 0; rain < _rainCnt; rain++)
        {
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Criminal_MoneyRain);
            _moneyList.Clear();
            for (int i = 0; i < _moneyCnt; i++)
            {
                // 동전 스폰
                float randX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
                float randY = Random.Range(bossData._rightTop.y-_topSpawnRange, bossData._rightTop.y);
                GameObject money = Instantiate(_moneyPrefab, new Vector2(randX, randY), Quaternion.identity);
                float randGravity = Random.Range(_moneyMinGravity, _moneyMaxGravity);
                money.GetComponent<Rigidbody2D>().gravityScale = randGravity;
                _moneyList.Add(money);
            }

            yield return new WaitForSeconds(_rainInterval);
        }
    }
}

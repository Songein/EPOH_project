using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject _vaccinePrefab;
    [SerializeField] private GameObject _virusPrefab;
    [SerializeField] private int _objectCnt; // 한번에 떨어지는 오브젝트 개수
    [SerializeField] private int _fallingCnt; // 떨어지는 횟수
    [SerializeField] private int _fallingInteraval; // 다음 떨어지기까지의 간격
    
    [SerializeField] private float _topSpawnRange; // top에서부터 스폰되는 범위
    [SerializeField] private float _minGravity;
    [SerializeField] private float _maxGravity;
    [SerializeField] private float _fallingSpeed;
    public void Activate()
    {
        BossData bossData = BossManagerNew.Instance.bossData;
        
        // 랜덤한 위치에 바이러스와 백신 생성
        StartCoroutine(SpawnObject(_vaccinePrefab, bossData));
        StartCoroutine(SpawnObject(_virusPrefab, bossData));
    }

    IEnumerator SpawnObject(GameObject obj, BossData bossData)
    {
        for (int fall = 0; fall < _fallingCnt; fall++)
        {
            for (int i = 0; i < _objectCnt; i++)
            {
                float randX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
                float randY = Random.Range(bossData._rightTop.y-_topSpawnRange, bossData._rightTop.y);
                GameObject _gameObject = Instantiate(obj, new Vector2(randX, randY), Quaternion.identity);
                float randGravity = Random.Range(_minGravity, _maxGravity) * _fallingSpeed;
                _gameObject.GetComponent<Rigidbody2D>().gravityScale = randGravity;
            }

            yield return new WaitForSeconds(_fallingInteraval);
        }
    }
}

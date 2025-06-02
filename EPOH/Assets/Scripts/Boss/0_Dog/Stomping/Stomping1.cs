using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomping1 : MonoBehaviour, BossSkillInterface
{
    //BossDog 프리팹
    [SerializeField] private GameObject _dogPrefab;
    [SerializeField] private GameObject _shockWavePrefab;
    private GameObject _dog;
    
    //Stomping1 스킬 변수
    [SerializeField] private int shockWaveCnt = 4;
    [SerializeField] private float facingTime = 0.5f; //플레이어를 바라보는 시간
    [SerializeField] private float precursorTime = 1.5f; //발구르기 일반 공격 전조 시간
    [SerializeField] private float waveTime = 0.5f; //충격파 간 간격 시간
    
    //충격파 관련 변수
    [SerializeField] private float firstWavePos;
    [SerializeField] private float shockWaveY;
    [SerializeField] private float waveTerm;
    
    public void Activate()
    {
        StartCoroutine(StartStomping1());
    }
    
    public IEnumerator StartStomping1()
    {
        //개 그림자가 플레이어 위치에 생성
        Vector2 playerPos = BossManagerNew.Current.player.transform.position;
        SpawnDog(playerPos);
        
        //facingTime 이후 플레이어가 위치한 방향을 바라본다
        yield return new WaitForSeconds(facingTime);
        BossManagerNew.Current.IsPlayerRight(_dog.transform);

        yield return new WaitForSeconds(precursorTime);

        _dog.GetComponent<Animator>().SetTrigger("Stomping1");

        //stomping wave 오브젝트 활성화
        yield return new WaitForSeconds(0.7f);

        // 보스가 왼쪽을 바라보고 있는지 오른쪽을 바라보고 있는지 체크
        GameObject shockWaveParent;
        if (_dog.GetComponent<SpriteRenderer>().flipX)
        {
            // 오른쪽
            shockWaveParent = _dog.transform.GetChild(1).gameObject;
        }
        else
        {
            // 왼쪽
            shockWaveParent = _dog.transform.GetChild(0).gameObject;
        }
        
        foreach (Transform shockwave in shockWaveParent.transform)
        {
            shockwave.gameObject.SetActive(true);
            yield return new WaitForSeconds(waveTime);
        }

        yield return new WaitForSeconds(waveTime);
        Destroy(_dog);
    }
    
    void SpawnDog(Vector2 spawnPoint)
    {
        _dog = null;
        GameObject boss = Instantiate(_dogPrefab, spawnPoint, Quaternion.identity);
        _dog = boss;
    }
}

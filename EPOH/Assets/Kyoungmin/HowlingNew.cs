using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HowlingNew : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint1; //스폰 포인트1(중앙)
    [SerializeField] private Vector3 spawnPoint2; //스폰 포인트2(왼쪽)
    [SerializeField] private Vector3 spawnPoint3; //스폰 포인트3(오른쪽)
    [SerializeField] private float shockWaveStartTime = 2f; //충격파 발생까지의 시간

    [SerializeField] private float absorbingDuration = 5f; //플레이어를 빨아들이는 스킬 지속시간
    [SerializeField] private float absorbPower; //빨아들이는 힘
    private bool absorbFlag; //빨아드리는 스킬 플래그
    [SerializeField] private float explosionStartTime = 0.5f; //폭발 시작까지의 시간
    
    private int random; //등장 위치를 결정할 난수(하울링1)
    [SerializeField] private GameObject bossPrefab; //boss prefab
    private List<GameObject> bossList = new List<GameObject>();
    private Rigidbody2D playerRD;

    private void Start()
    {
        playerRD = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (absorbFlag)
        {
            float v = Mathf.Floor(Mathf.Sqrt(Mathf.Abs(playerRD.transform.position.x - this.transform.position.x)) * 10f) / 100f;
            Debug.Log(absorbPower * (1-v));
            Vector2 direction;
            if (IsPlayerRight())
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }
            playerRD.AddForce(direction * absorbPower * (1-v));
        }
    }

    //일반 하울링 스킬 호출 함수
    public void Howling1()
    {
        //중앙(1체), 맵 양 끝(2체) 중 등장할 곳 결정
        random = Random.Range(0, 2);
        if (random == 0)
        {
            //중앙에 보스 생성
            bossList.Add(Instantiate(bossPrefab,spawnPoint1,Quaternion.identity));
        }
        else if (random == 1)
        {
            //맵 양 끝에 보스 생성
            bossList.Add(Instantiate(bossPrefab,spawnPoint2,Quaternion.identity));
            bossList.Add(Instantiate(bossPrefab,spawnPoint3,Quaternion.identity));
        }
        //고개를 드는 애니메이션으로 전환
        Debug.Log("개 그림자 고개를 든다.");
        //2초 후에 충격파 발생
        StartCoroutine(ShockWave());

    }
    
    //강화 하울링 호출 함수
    public void Howling2()
    {
        //고개를 드는 애니메이션으로 전환
        Debug.Log("개 그림자 고개를 든다.");
        //개 방향으로 빨려들어가는 이펙트
        Debug.Log("개 방향으로 빨려들어가는 이펙트.");
        //플레이어에게 개 방향으로 빨려들어가는 힘 작용(개와 가까워질수록 빨려들어가는 힘 강화)
        absorbFlag = true;
        //5초 후, 빨려들어가는 힘 사라지고 0.5초 후에 폭발 이펙트 발생
        StartCoroutine(ExplosionWave());
    }
    
    //충격파 생성 코루틴
    IEnumerator ShockWave()
    {
        //충격파 전조 시간이 지나고
        yield return new WaitForSeconds(shockWaveStartTime);
        //충격파 발생
        if (bossList.Count == 1)
        {
            bossList[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            bossList[0].transform.GetChild(0).gameObject.SetActive(true);
            bossList[1].transform.GetChild(0).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.41f);
        for (int i = 0; i < bossList.Count; i++)
        {
            Destroy(bossList[i]);
        }
        bossList.Clear();
    }

    IEnumerator ExplosionWave()
    {
        yield return new WaitForSeconds(absorbingDuration);
        absorbFlag = false;
        yield return new WaitForSeconds(explosionStartTime);
        
        GameObject explosionWave = transform.GetChild(1).gameObject;
        explosionWave.SetActive(true);
    }
    bool IsPlayerRight()
    {
        if (playerRD.transform.position.x - this.transform.position.x >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

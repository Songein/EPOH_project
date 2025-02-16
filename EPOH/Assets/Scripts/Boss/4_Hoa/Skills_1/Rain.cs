using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private int minRaindrops;
    [SerializeField] private int maxRaindrops;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject rainPrefab;


    // [SerializeField] private float growSpeed;
    // [SerializeField] private float maxLength;


    public void Activate()
    {
        if (BossManagerNew.Current == null)
        {
            Debug.LogError("BossManagerNew instance is not found!");
            return;
        }
      

        BossData bossData = BossManagerNew.Current.bossData;
        if (bossData == null)
        {
            Debug.LogError("BossData is not assigned in BossManagerNew!");
            return;
        }

        StartCoroutine(rain(bossData));
    }
    IEnumerator rain(BossData bossData)
    {
        int raindropCount = Random.Range(minRaindrops, maxRaindrops + 1); // 랜덤 개수 결정
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < raindropCount; j++)
            {
                float spawnX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x); // 랜덤 위치
                Vector3 spawnPosition = new Vector3(spawnX, bossData._rightTop.y, 0);
                GameObject rainDrop = Instantiate(rainPrefab, spawnPosition, Quaternion.identity);

                float fallSpeed = Random.Range(minSpeed, maxSpeed); // 랜덤 속도
                Rigidbody2D rb = rainDrop.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = new Vector2(0, -fallSpeed); // 아래로 떨어지는 속도 설정
                }
            }
            yield return new WaitForSeconds(1f);
        }
        


        



    }

    /*
    public void ClearAllRaindrops()
    {
        GameObject[] raindrops = GameObject.FindGameObjectsWithTag("RainDrop");
        foreach (GameObject drop in raindrops)
        {
            Destroy(drop);
        }

    }
    */
}



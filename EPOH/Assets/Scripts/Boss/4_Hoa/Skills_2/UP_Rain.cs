using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Rain : MonoBehaviour
{
    [SerializeField] private int minRaindrops;
    [SerializeField] private int maxRaindrops;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject rainPrefab;
    [SerializeField] private GameObject safePrefab;


    // [SerializeField] private float growSpeed;
    // [SerializeField] private float maxLength;

    private void Start()
    {
     
    }
    public void Activate()
    {
        if (BossManagerNew.Current == null)
        {
            Debug.LogError("BossManagerNew Current is not found!");
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

        float spawnX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
        float spawnY = Random.Range(bossData._leftBottom.y, bossData._leftBottom.y +10); // 랜덤 위치
        int raindropCount = Random.Range(minRaindrops, maxRaindrops + 1);  // 랜덤 개수 결정
        Vector3 safePosition = new Vector3(spawnX, bossData._leftBottom.y + 2, 0);
        GameObject safeZone = Instantiate(safePrefab, safePosition, Quaternion.Euler(0,0,-90));

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 3; i++)
        {
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Rain);
            for (int j = 0; j < raindropCount; j++)
            {
                 spawnX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x); // 랜덤 위치
                Vector3 spawnPosition = new Vector3(spawnX, bossData._rightTop.y, 0);
           /*
                float soundDelay = Random.Range(0.0005f, 0.05f);  // 랜덤
                yield return new WaitForSeconds(soundDelay);
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Rain);
               */
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

        Destroy(safeZone);

        BossManagerNew.Current.OnSkillEnd?.Invoke();

    }


}

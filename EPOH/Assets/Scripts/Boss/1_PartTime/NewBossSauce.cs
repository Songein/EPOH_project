using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossSauce : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private float maxLength;
    [SerializeField] private float growSpeed;
    [SerializeField] private GameObject ketchup;
    [SerializeField] private GameObject mustard;
    [SerializeField] private GameObject ketchupSauce;
    [SerializeField] private GameObject mustardSauce;

    public List<GameObject> lidList;
    public List<GameObject> sauceList;

    public void Activate() {
        StartCoroutine(CreateSauce());
    }

    private IEnumerator CreateSauce()
    {
        BossData bossData = BossManagerNew.Current.bossData;

        lidList.Clear(); // 리스트 비우기
        sauceList.Clear(); // 리스트 비우기


        for (int i = 0; i < 7; i++)
        {
            int num = Random.Range(0, 2);
            //bossData._leftBottom.x + 3 + i * 6 수정 전
            float xPos = Mathf.Lerp(bossData._leftBottom.x + 3, bossData._rightTop.x - 3, (float)i / 6);
            Vector3 saucePosition = new Vector3(xPos, bossData._rightTop.y -7, 0);

            GameObject prefab = (num == 0) ? ketchup : mustard;
            GameObject lid = Instantiate(prefab, saucePosition, Quaternion.identity);

            lidList.Add(lid);
        }


        yield return new WaitForSeconds(1f); // 소스 기다리는 시간

        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Sauce); //소리
        for (int i = 0; i < 7; i++)
        {
            float xPos = Mathf.Lerp(bossData._leftBottom.x + 3, bossData._rightTop.x - 3, (float)i / 6);

            Vector3 saucePrefabPosition = new Vector3(xPos, bossData._rightTop.y -7, 0);
            string name = lidList[i].name;
            GameObject saucePrefab = (name.Contains("Ketchup")) ? ketchupSauce : mustardSauce;
            GameObject sauce = Instantiate(saucePrefab, saucePrefabPosition, Quaternion.identity);

            sauceList.Add(sauce);

            //sauce.transform.localScale = new Vector3(1f, 0f, 1f);

            /*

            // 물줄기가 점점 아래로 길어지는 애니메이션
            while (sauce.transform.localScale.y < maxLength)
            {
                // 크기 증가 (아래로 확장)
                sauce.transform.localScale += new Vector3(0f, growSpeed * Time.deltaTime, 0f);

                // 위치 고정: 물줄기가 아래로만 길어지도록 보정
                sauce.transform.position = new Vector3(
                    sauce.transform.position.x,
                    saucePrefabPosition.y - (sauce.transform.localScale.y / 2), // 아래로 확장
                    sauce.transform.position.z
                );

            }
            yield return null; // 다음 프레임까지 대기
        }
            */
        }
            yield return new WaitForSeconds(2f);
            foreach (GameObject lid in lidList)
            {
                Destroy(lid); // 씬에서 제거
            }
            foreach (GameObject sauce in sauceList)
            {
                Destroy(sauce); // 씬에서 제거
            }
    }
}

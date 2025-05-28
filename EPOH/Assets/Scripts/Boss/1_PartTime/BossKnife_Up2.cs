using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnife_Up2 : MonoBehaviour
{
    public Transform player; // 플레이어 게임 오브젝트

    // 추적 변수
    public GameObject knifePrefab; // knife 프리팹

    private List<GameObject> knives = new List<GameObject>(); // knife 오브젝트 리스트
    private Animator knifeAnimator; // knife 애니메이터
    
    // void Update()
    // {
    //     // 매 프레임마다 눈동자가 플레이어의 x축과 y축을 따라다니도록 설정
    //     if (tracking_eye != null && player != null)
    //     {
    //         tracking_eye.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, tracking_eye.transform.position.z);
    //     }
    // }

    public void Activate()
    {
        // 전에 만든 나이프 제거
        foreach (var knife in knives)
        {
            if (knife != null)
                Destroy(knife);
        }
        knives.Clear(); // 리스트 초기화

        StartCoroutine(KnifeUp());
    }

    public IEnumerator KnifeUp()
    {
        player = BossManagerNew.Current.player.transform;
        float trackingDuration = 2.0f; // 추적 지속 시간
        float fallSpeed = 15.0f; // 나이프 낙하 속도

        // 나이프 3개 생성
        for (int i = 0; i < 3; i++)
        {
            Vector3 knifePosition = new Vector3(player.position.x + (i - 1) * 2.0f, player.position.y + 8.0f, player.position.z);
            GameObject knife = Instantiate(knifePrefab, knifePosition, Quaternion.identity);

            // 크기 조정
            knife.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Animator 컴포넌트 가져오기
            knifeAnimator = knife.GetComponent<Animator>();
            if (knifeAnimator == null)
            {
                Debug.LogError("Animator component is missing on the knife prefab.");
            }

            knives.Add(knife);
        }

        // 추적 단계
        float elapsedTime = 0f;
        while (elapsedTime < trackingDuration)
        {
            for (int i = 0; i < knives.Count; i++)
            {
                if (knives[i] != null && player != null)
                {
                    knives[i].transform.position = new Vector3(player.position.x + (i - 1) * 2.0f, player.position.y + 8.0f, knives[i].transform.position.z);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 나이프 애니메이션 재생
        foreach (var knife in knives)
        {
            if (knifeAnimator != null)
            {
                knifeAnimator.Play("Knife_Tracking"); // Animator에 설정된 애니메이션 이름
            }
        }

        // 낙하 단계
        //Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, knives[0].transform.position.z);

        for (int i = 0; i < knives.Count; i++)
        {
         knives[i].transform.position = Vector3.MoveTowards(knives[i].transform.position, new Vector3(player.position.x + (i - 1) * 2.0f, player.position.y + 5, knives[i].transform.position.z), fallSpeed * Time.deltaTime);
        }



    }
}

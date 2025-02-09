using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnifeUp : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트

     // 추적 변수
    public GameObject knifePrefab; // knife 프리팹

    private List<GameObject> knives = new List<GameObject>(); // knife 오브젝트 리스트
    private Animator knifeAnimator; // knife 애니메이터


    private void Awake()
    {
        dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        player = dog._player;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // 나이프 3개 생성
        for (int i = 0; i < 3; i++)
        {
            Vector3 knifePosition = new Vector3(player.transform.position.x + (i - 1) * 2.0f, player.transform.position.y + 8.0f, player.transform.position.z);
            GameObject knife = Instantiate(knifePrefab, knifePosition, Quaternion.identity);
            knife.SetActive(false); // 시작 시 비활성화

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

    }

    
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
        // 각 나이프 활성화
        for (int i = 0; i < knives.Count; i++)
        {
            knives[i].SetActive(true);
        }

        StartCoroutine(KnifeUp());
    }

    public IEnumerator KnifeUp()
    {
        float trackingDuration = 2.0f; // 추적 지속 시간
        float fallSpeed = 15.0f; // 나이프 낙하 속도

        // 추적 단계
        float elapsedTime = 0f;
        while (elapsedTime < trackingDuration)
        {
            for (int i = 0; i < knives.Count; i++)
            {
                if (knives[i] != null && player != null)
                {
                    knives[i].transform.position = new Vector3(player.transform.position.x + (i - 1) * 2.0f, player.transform.position.y + 8.0f, knives[i].transform.position.z);
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
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, knives[0].transform.position.z);
        while (knives[0].transform.position.y > targetPosition.y)
        {
            for (int i = 0; i < knives.Count; i++)
            {
                knives[i].transform.position = Vector3.MoveTowards(knives[i].transform.position, new Vector3(player.transform.position.x + (i - 1) * 2.0f, player.transform.position.y, knives[i].transform.position.z), fallSpeed * Time.deltaTime);
            }
            yield return null;
        }

        // 애니메이션이 끝났는지 확인하고 나이프 삭제
        foreach (var knife in knives)
        {
            // 애니메이션이 끝났는지 확인
            if (knifeAnimator != null)
            {
                if (knifeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    knives[0].SetActive(false);
                    knives[1].SetActive(false);
                    knives[2].SetActive(false);
                }
            }
        }
    }
}

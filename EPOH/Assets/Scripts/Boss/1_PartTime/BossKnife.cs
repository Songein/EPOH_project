using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnife : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트

     // 추적 변수
    public GameObject knifePrefab; // 나이프 프리팹

    private GameObject knife; // 나이프 오브젝트
    private Animator knifeAnimator; // 나이프 애니메이터


    private void Awake()
    {
        dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        player = dog._player;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // knife 오브젝트 생성
        Vector3 knifePosition = new Vector3(player.transform.position.x, player.transform.position.y + 8.0f, player.transform.position.z);
        knife = Instantiate(knifePrefab, knifePosition, Quaternion.identity);
        knife.SetActive(false); // 시작 시 비활성화

        // 크기 조정
        knife.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Animator 컴포넌트 가져오기
        knifeAnimator = knife.GetComponent<Animator>();
        if (knifeAnimator == null)
        {
            Debug.LogError("Animator component is missing on the knife prefab.");
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
        knife.SetActive(true); // knife 활성화
        StartCoroutine(Knife());
    }

    public IEnumerator Knife()
    {
        float trackingDuration = 2.0f; // 추적 지속 시간
        float fallSpeed = 15.0f; // 나이프 낙하 속도

        // 추적 단계
        float elapsedTime = 0f;
        while (elapsedTime < trackingDuration)
        {
            if (knife != null && player != null)
            {
                knife.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 8.0f, knife.transform.position.z);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 나이프 애니메이션 재생
        if (knifeAnimator != null)
        {
            knifeAnimator.Play("Knife_Tracking"); // Animator에 설정된 애니메이션 이름
        }

        // 낙하 단계
        if (knife != null)
        {
            Vector3 targetPosition = new Vector3(knife.transform.position.x, player.transform.position.y, knife.transform.position.z);
            while (knife.transform.position.y > targetPosition.y)
            {
                knife.transform.position = Vector3.MoveTowards(knife.transform.position, targetPosition, fallSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // knife 비활성화
        knife.SetActive(false);
    }
}

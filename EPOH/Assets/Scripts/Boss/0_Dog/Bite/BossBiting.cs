using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBiting : MonoBehaviour, BossSkillInterface
{
    public GameObject player; // 플레이어 게임 오브젝트

    public GameObject biteReadyPrefab; // Bite Ready 프리팹
    public GameObject bitePrefab; // Bite 프리팹

    public float shadow_speed = 10.0f; // 그림자 이동 속도

    //물기 변수
    [SerializeField] float reach_distance_short; // 포물선 이동 거리
    [SerializeField] float Dog_yposition; // 이동할 y 위치
    [SerializeField] float bite_duration = 0.5f; //포물선 이동에 걸리는 시간

    public AnimationCurve curve; // 포물선 이동을 위한 AnimationCurve 선언

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }


    public void Activate()
    {
        StartCoroutine(Biting());
    }

    private void AdjustDirection(GameObject obj, Vector3 playerPos, Vector3 unifiedScale)
    {
        // 크기를 통일
        obj.transform.localScale = unifiedScale;

        if (playerPos.x < obj.transform.position.x)
        {
            // 왼쪽을 바라보도록 설정
            obj.transform.localScale = new Vector3(
                Mathf.Abs(obj.transform.localScale.x),
                obj.transform.localScale.y,
                obj.transform.localScale.z
            );
        }
        else
        {
            // 오른쪽을 바라보도록 설정
            obj.transform.localScale = new Vector3(
                -Mathf.Abs(obj.transform.localScale.x),
                obj.transform.localScale.y,
                obj.transform.localScale.z
            );
        }
    }


    public IEnumerator Biting()
    {
        // 그림자 오브젝트 생성
        float middlePoint = (BossManagerNew.Current.bossData._rightTop.x - BossManagerNew.Current.bossData._leftBottom.x) / 2;
        Vector3 shadowStartPosition = new Vector3(middlePoint, transform.position.y, transform.position.z);
        GameObject bite_ready_object = Instantiate(biteReadyPrefab, shadowStartPosition, Quaternion.identity);

        // 통일된 크기 정의
        Vector3 unifiedScale = new Vector3(
            bite_ready_object.transform.localScale.x * 0.5f,
            bite_ready_object.transform.localScale.y * 0.5f,
            bite_ready_object.transform.localScale.z
        );

        // 좌우 반전 효과 (잠깐 반전)
        for (int i = 0; i < 2; i++) // 두 번 반전
        {
            bite_ready_object.transform.localScale = new Vector3(
                -unifiedScale.x, // 반전된 방향의 x축 크기
                unifiedScale.y,   // y축 크기는 유지
                unifiedScale.z    // z축 크기는 유지
            );
            yield return new WaitForSeconds(0.8f); // 0.8초 대기
        }
        
       // 플레이어 방향에 따라 bite_ready_object 방향 설정
        AdjustDirection(bite_ready_object, player.transform.position, unifiedScale);

        // 반전된 상태에서 1초 동안 대기
        yield return new WaitForSeconds(1.5f);
        Debug.Log("플레이어를 바라보며 으르렁대는 모션");

        // bite_ready_object 삭제
        Destroy(bite_ready_object);
        
        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;
        //그림자의 시작 위치 파악
        Vector2 start = shadowStartPosition;

        // 그림자의 도착지점 위치 지정
        Vector2 end = new Vector2(player_pos.x, Dog_yposition);

        GameObject bite_object = Instantiate(bitePrefab, start, Quaternion.identity); // bite 프리팹

        Animator biteAnimator = bite_object.GetComponent<Animator>();

        // 플레이어 방향에 따라 bite_object 방향 설정
        AdjustDirection(bite_object, player.transform.position, unifiedScale);

        if (biteAnimator == null)
        {
            Debug.LogError("Animator component is missing on the bitePrefab.");
            yield break;
        }

        biteAnimator.Play("Biting"); // 돌진 준비 애니메이션

        //해당 위치로 아치형을 그리며 그림자를 돌진
        float time = 0f;
        while (time < bite_duration)
        {
            time += Time.deltaTime;
            float linearT = time / bite_duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 8f, heightT);

            bite_object.transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            if (time > bite_duration / 2 && biteAnimator.GetCurrentAnimatorStateInfo(0).IsName("BiteReady"))
            {
                biteAnimator.Play("Bite"); // 물기 애니메이션
            }

            yield return null;
        }

        // 그림자를 y축 -9로 내려오도록 설정
        Vector2 groundPosition = new Vector2(bite_object.transform.position.x, -9f);
        while (bite_object.transform.position.y > groundPosition.y)
        {
            bite_object.transform.position = new Vector2(bite_object.transform.position.x, Mathf.MoveTowards(bite_object.transform.position.y, groundPosition.y, Time.deltaTime * 10.0f));
            yield return null;
        }
        

        // 땅에 도착한 후 잠시 대기
        yield return new WaitForSeconds(0.1f);
        

        // 그림자 오브젝트 삭제
        Destroy(bite_object);
        
        yield return new WaitForSeconds(0.2f);
    }
    
}

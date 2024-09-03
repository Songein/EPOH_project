using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBiting : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트
    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    public float shadow_speed = 10.0f; // 그림자 이동 속도

    //물기 변수
    [SerializeField] float reach_distance_short; // 포물선 이동 거리
    [SerializeField] float Dog_yposition; // 이동할 y 위치
    [SerializeField] float bite_duration = 0.5f; //포물선 이동에 걸리는 시간
    [SerializeField] private GameObject[] bite_effects; //bite effect 배열
    [SerializeField] SpriteRenderer bite_renderer;

    public AnimationCurve curve; //포물선 이동을 위한 AnimationCurve 선언

    private void Awake()
    {
        dog = GetComponent<BossDogController>();
        player = dog._player;
        
    }


    public void Activate()
    {
        StartCoroutine(Biting());
    }

    private IEnumerator Biting()
    {
        // 그림자 오브젝트 생성
        Vector3 shadowStartPosition = dog.spawnMiddlePoint;
        GameObject shadow_object = Instantiate(dog.bossPrefab, shadowStartPosition, Quaternion.identity);
        Debug.Log("왼쪽으로 무는 이펙트");

        // 처음에는 그림자를 그대로 둠
        shadow_object.transform.localScale = new Vector3(Mathf.Abs(shadow_object.transform.localScale.x), shadow_object.transform.localScale.y, shadow_object.transform.localScale.z);

        // 그림자가 잠시 나타난 후 반전시킴
        yield return new WaitForSeconds(0.8f); // 0.5초 후 반전
        shadow_object.transform.localScale = new Vector3(-shadow_object.transform.localScale.x, shadow_object.transform.localScale.y, shadow_object.transform.localScale.z); // 그림자 반전
        Debug.Log("오른쪽으로 무는 이펙트");

        // 그림자가 반전된 후 다시 0.5초 동안 기다림
        yield return new WaitForSeconds(0.8f);

        // 플레이어 위치에 따라 그림자를 반전시킴
        dog.IsPlayerRight(shadow_object);

        // 반전된 상태에서 1초 동안 대기
        yield return new WaitForSeconds(1.5f);
        Debug.Log("플레이어를 바라보며 으르렁대는 모션");
        
        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;
        //그림자의 시작 위치 파악
        Vector2 start = shadow_object.transform.position;
        float bite_distance = 0f; //그림자가 이동할 거리

        if (shadow_object.transform.position.x - player_pos.x >= 0f)
        {
            //포물선 이동을 왼쪽으로 사정거리만큼 하도록 설정
            bite_distance = -1 * reach_distance_short;
            //보스의 스프라이트를 왼쪽 방향으로 설정
            bite_renderer.flipX = false;
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance_short;
            //보스의 스프라이트를 오른쪽 방향으로 설정
            bite_renderer.flipX = true;
        }

        // 그림자의 도착지점 위치 지정
        Vector2 end = new Vector2(player_pos.x, Dog_yposition);

        //해당 위치로 아치형을 그리며 그림자를 돌진
        float time = 0f;
        while (time < bite_duration)
        {
            time += Time.deltaTime;
            float linearT = time / bite_duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 10f, heightT);

            shadow_object.transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }

        // 그림자를 y축 -4로 내려오도록 설정
        Vector2 groundPosition = new Vector2(shadow_object.transform.position.x, -4f);
        while (shadow_object.transform.position.y > -4f)
        {
            shadow_object.transform.position = new Vector2(shadow_object.transform.position.x, Mathf.MoveTowards(shadow_object.transform.position.y, -4f, Time.deltaTime * 10.0f));
            yield return null;
        }
        

        // 땅에 도착한 후 잠시 대기
        yield return new WaitForSeconds(0.1f);
        

        // 그림자 오브젝트 삭제
        Destroy(shadow_object);
    }
    
}

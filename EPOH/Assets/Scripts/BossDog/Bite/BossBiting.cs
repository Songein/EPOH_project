using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBiting : MonoBehaviour
{
    public GameObject player; // 플레이어 게임 오브젝트
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    [SerializeField] float reach_distance_short; // 포물선 이동 거리
    [SerializeField] float Dog_min_area; // 이동 가능 최소 x 위치
    [SerializeField] float Dog_max_area; // 이동 가능 최대 x 위치
    [SerializeField] float Dog_yposition; // 이동할 y 위치

    public GameObject shadow_prefab; // 그림자 오브젝트 프리팹
    public float shadow_speed = 10.0f; // 그림자 이동 속도

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //물기 변수
    public GameObject bite_area; //bite 공격 범위 오브젝트
    public AnimationCurve curve; //포물선 이동을 위한 AnimationCurve 선언
    [SerializeField] float bite_duration = 0.5f; //포물선 이동에 걸리는 시간
    [SerializeField] private GameObject[] bite_effects; //bite effect 배열
    [SerializeField] SpriteRenderer bite_renderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인

        
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }

        // Scene의 가장 왼쪽과 오른쪽 좌표를 설정
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        
    }

    public void Bite()
    {
        StartCoroutine(Biting());
    }

    private IEnumerator Biting()
    {
        // 그림자 오브젝트 생성
        Vector3 shadowStartPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z); // y 축 위치를 0.5 아래로 설정
        GameObject shadow_object = Instantiate(shadow_prefab, shadowStartPosition, Quaternion.identity);
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
        if (player.transform.position.x > shadowStartPosition.x)
        {
            shadow_object.transform.localScale = new Vector3(-Mathf.Abs(shadow_object.transform.localScale.x), shadow_object.transform.localScale.y, shadow_object.transform.localScale.z); // 그림자 반전
        }
        else
        {
            shadow_object.transform.localScale = new Vector3(Mathf.Abs(shadow_object.transform.localScale.x), shadow_object.transform.localScale.y, shadow_object.transform.localScale.z); // 그림자 그대로
        }

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
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(false);
            // 안전하게 배열에 접근
            if (bite_effects.Length > 0)
            {
                bite_effects[0].SetActive(true);
            }
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance_short;
            //보스의 스프라이트를 오른쪽 방향으로 설정
            bite_renderer.flipX = true;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(true);
            // 안전하게 배열에 접근
            if (bite_effects.Length > 1)
            {
                bite_effects[1].SetActive(true);
            }
        }

        // 그림자의 도착지점 위치 지정
        Vector2 end = new Vector2(player_pos.x, Dog_yposition);
        //bite area 오브젝트 활성화(공격 범위 활성화)
        bite_area.SetActive(true);

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
        
        // bite area 비활성화
        bite_area.SetActive(false);

        if (bite_effects.Length > 0)
        {
            bite_effects[0].SetActive(false);
        }

        if (bite_effects.Length > 1)
        {
            bite_effects[1].SetActive(false);
        }

        // 그림자 오브젝트 삭제
        Destroy(shadow_object);
    }
    


    private void OnTriggerEnter2D(Collider2D collision) // 충돌시 데미지
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌");

            //PlayerHealth 스크립트 할당
            player_health = collision.gameObject.GetComponent<PlayerHealth>();
            player_health.Damage(attack_power); //보스의 공격 세기만큼 플레이어의 hp 감소
        }
    }
    
}

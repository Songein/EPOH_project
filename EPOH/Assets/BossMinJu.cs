using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinJu : MonoBehaviour
{
    public GameObject player; // 플레이어 게임 오브젝트
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    [SerializeField] float attack_power = 8f; // 보스 공격 세기

    [SerializeField] float reach_distance_short; // 포물선 이동 거리
    [SerializeField] float Dog_min_area; // 이동 가능 최소 x 위치
    [SerializeField] float Dog_max_area; // 이동 가능 최대 x 위치
    [SerializeField] float Dog_yposition; // 이동할 y 위치

    public GameObject shadow_prefab; // 그림자 오브젝트 프리팹
    public float shadow_speed = 10.0f; // 그림자 이동 속도


    //달리기 변수
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private SpriteRenderer warning_renderer;
    public float warning_duration = 3.0f; // 전조 영역 지속 시간
    public Color warning_color = Color.red; // 전조 영역 색상

    //할퀴기 변수
    public GameObject scratch_prefab; // 할퀸 자국 프리팹
    public GameObject scratch_particle_prefab; // 할퀸 자국 폭발 이펙트 프리팹

    //추적 변수
    public GameObject tracking_eye_prefab; // 추적 눈동자 프리팹
    public GameObject tracking_effect_prefab; // 추적 이펙트 프리팹
    public float tracking_effect_speed = 5.0f; // 추적 이펙트의 속도

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

        // 씬의 가장 왼쪽과 오른쪽 좌표를 설정 (카메라 뷰포트를 기준으로 계산)
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
    }

    public void Run()
    {
        StartCoroutine(Running());
    }

    private IEnumerator Running()
    {
        // 현재 오브젝트의 위치에 따라 이동할 방향을 결정
        Transform objTransform = this.transform;
        Vector3 targetPosition = objTransform.position.x < (rightEdge.x + leftEdge.x) / 2 ? rightEdge : leftEdge;
        Vector3 startPosition = objTransform.position;
        
        // 전조 영역 생성 및 유지
        GameObject warning_area = new GameObject("WarningArea");
        warning_area.transform.position = new Vector3((startPosition.x + targetPosition.x) / 2, objTransform.position.y, objTransform.position.z);
        warning_renderer = warning_area.AddComponent<SpriteRenderer>();

        // 전조 영역의 색상 
        Color warningColorWithAlpha = new Color(warning_color.r, warning_color.g, warning_color.b, 0.5f); // 알파값 0.5 설정
        warning_renderer.color = warningColorWithAlpha;
        warning_renderer.sortingOrder = 20; // 오브젝트 위로 표시되도록 순서 설정
        warning_renderer.sprite = CreateWarningSprite(); // 
        
        // 전조 영역의 크기 설정 (오브젝트가 이동할 거리만큼)
        float warning_width = Mathf.Abs(targetPosition.x - startPosition.x);
        warning_renderer.transform.localScale = new Vector3(warning_width, objTransform.localScale.y, 1);

        yield return new WaitForSeconds(warning_duration); // 전조 영역 유지

        Destroy(warning_area); // 전조 영역 삭제

        // 그림자 오브젝트 생성 및 이동
        GameObject shadow_object = Instantiate(shadow_prefab, objTransform.position, Quaternion.identity);
        Vector3 shadow_target_position = new Vector3(targetPosition.x, shadow_object.transform.position.y, shadow_object.transform.position.z);
        
        // 그림자의 y 좌표를 고정하여 수평으로만 이동하도록 설정
        float fixedY = objTransform.position.y;

        while (Vector3.Distance(shadow_object.transform.position, shadow_target_position) > 0.1f)
        {
            shadow_object.transform.position = Vector3.MoveTowards(shadow_object.transform.position, new Vector3(shadow_target_position.x, fixedY, shadow_target_position.z), shadow_speed * Time.deltaTime);
            yield return null;
        }

        Destroy(shadow_object); // 그림자 오브젝트 삭제
    }

    // 기본 사각형 스프라이트 생성 함수
    private Sprite CreateWarningSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();

        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        Vector2 pivot = new Vector2(0.5f, 0.5f);

        return Sprite.Create(texture, rect, pivot, 1.0f);
    }

    public void Scratch()
    {
        StartCoroutine(Scratching());
    }

    private IEnumerator Scratching()
    {
        int scratch_count = 3; // 총 3번의 할퀸 자국을 생성

    for (int i = 0; i < scratch_count; i++)
    {
        // 할퀸 자국 프리팹 생성
        Vector3 scratch_position = new Vector3(Random.Range(leftEdge.x, rightEdge.x), player.transform.position.y, 0);
        GameObject scratch_object = Instantiate(scratch_prefab, scratch_position, Quaternion.identity); // 프리팹 사용

        // 초기 투명한 빨간색 설정
        SpriteRenderer scratch_renderer = scratch_object.GetComponent<SpriteRenderer>();

        scratch_renderer.sortingOrder = 10;

        Color initialColor = new Color(1, 0, 0, 0); // 투명한 빨간색
        scratch_renderer.color = initialColor;

        // 색상이 점점 진해짐
        float duration = 3.0f; // 3초간 색상 변화
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // 0에서 1로 알파 값 증가
            scratch_renderer.color = new Color(1, 0, 0, alpha); // 점점 진해지는 빨간색
            yield return null;
        }

        // 3초 후 폭발 효과 (여기서는 간단히 오브젝트 삭제로 처리)
        Destroy(scratch_object);

        Vector3 scratch_size = scratch_renderer.bounds.size; // 할퀸 자국의 크기 가져오기

        // 할퀸 자국 경계 위에 파티클 5개 생성
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
        for (int j = 0; j < 5; j++)
        {
            // 파티클이 할퀸 자국 경계 위에 고르게 배치되도록 설정
            float angle = j * Mathf.PI * 2 / 5; // 5개의 파티클을 원형으로 배치하기 위한 각도 계산
            float radius = Mathf.Min(scratch_size.x, scratch_size.y) / 2; // 할퀸 자국의 반지름 계산
            Vector3 particle_position = new Vector3(
                scratch_position.x + Mathf.Cos(angle) * radius,
                scratch_position.y + Mathf.Sin(angle) * radius,
                scratch_position.z
            );

            GameObject scratch_particle = Instantiate(scratch_particle_prefab, particle_position, Quaternion.identity);

            // SpriteRenderer를 통해 sortingOrder 설정
            SpriteRenderer particle_renderer = scratch_particle.GetComponent<SpriteRenderer>();
            particle_renderer.sortingOrder = 10;

            // 파티클이 땅으로 떨어지도록 중력 적용
            Rigidbody2D rb = scratch_particle.AddComponent<Rigidbody2D>();
            rb.gravityScale = 5.0f; // 중력 스케일 설정

            rigidbodies.Add(rb);

            Destroy(scratch_particle, 3.0f); // 3초 후 파티클 삭제
        }

        // 파티클이 y축 -4에 도달하면 멈추도록 설정
        bool allParticlesStopped = false;
        while (!allParticlesStopped)
        {
            allParticlesStopped = true;
            foreach (Rigidbody2D rb in rigidbodies)
            {
                if (rb != null && rb.transform.position.y <= -4f)
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                }
                else if (rb != null && rb.transform.position.y > -4f)
                {
                    allParticlesStopped = false; // 아직 멈추지 않은 파티클이 있다면 루프를 계속
                }
            }
            yield return null;
        }


        // 다음 할퀸 자국 생성 전 1초 대기
        if (i < scratch_count - 1)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }
    }
    
    public void Track()
    {
        StartCoroutine(Tracking());
    }

    private IEnumerator Tracking()
    {
        // 플레이어 위치의 y축 2에 추적 눈동자 프리팹 생성
        Vector3 eyePosition = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
        GameObject tracking_eye = Instantiate(tracking_eye_prefab, eyePosition, Quaternion.identity);

        // 플레이어가 바라보는 방향 뒤쪽에 추적 이펙트 프리팹 생성
        Vector3 effectSpawnPosition = player.transform.position - player.transform.right * 2.0f; // 플레이어 뒤쪽에 2단위 거리로 생성
        GameObject tracking_effect = Instantiate(tracking_effect_prefab, effectSpawnPosition, Quaternion.identity);

        float elapsedTime = 0f;
        float duration = 5.0f; // 추적 이펙트가 플레이어를 따라다니는 시간

        while (elapsedTime < duration)
        {
            tracking_eye.transform.position = new Vector3(player.transform.position.x, tracking_eye.transform.position.y, tracking_eye.transform.position.z);

            // 추적 이펙트가 플레이어의 방향으로 일정 속도로 따라가도록 설정
            Vector3 directionToPlayer = (player.transform.position - tracking_effect.transform.position).normalized;
            tracking_effect.transform.position += directionToPlayer * tracking_effect_speed * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 5초 후 추적 눈동자와 이펙트 제거
        Destroy(tracking_eye);
        Destroy(tracking_effect);

    }


    public void Bite()
    {
        StartCoroutine(Biting());
    }

    private IEnumerator Biting()
    {
        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;
        //보스의 시작 위치 파악
        Vector2 start = transform.position;
        float bite_distance = 0f; //보스가 이동할 거리

        //transform.position.x - player_pos.x 값이 양수면 플레이어는 보스의 왼쪽에 위치함.
        //반대로, 음수면 플레이어는 보스의 오른쪽에 위치함을 알 수 있음.
        if (transform.position.x - player_pos.x >= 0f)
        {
            //포물선 이동을 왼쪽으로 사정거리만큼 하도록 설정
            bite_distance = -1 * reach_distance_short;
            //보스의 스프라이트를 왼쪽 방향으로 설정
            bite_renderer.flipX = false;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(false);
            bite_effects[0].SetActive(true);
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance_short;
            //보스의 스프라이트를 오른쪽 방향으로 설정
            bite_renderer.flipX = true;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(true);
            bite_effects[1].SetActive(true);
        }

        //보스의 도착지점 위치 지정
        Vector2 end = new Vector2(Mathf.Clamp(transform.position.x + bite_distance, Dog_min_area, Dog_max_area), Dog_yposition);
        //bite area 오브젝트 활성화(공격 범위 활성화)
        bite_area.SetActive(true);

        //해당 위치로 아치형을 그리며 돌진
        float time = 0f;
        while (time < bite_duration)
        {
            time += Time.deltaTime;
            float linearT = time / bite_duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 10f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("[Bite] : bite area 비활성화");
        bite_area.SetActive(false);
        bite_effects[0].SetActive(false);
        bite_effects[1].SetActive(false);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScratching : MonoBehaviour
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

    //할퀴기 변수
    public GameObject scratch_prefab; // 할퀸 자국 프리팹
    public GameObject scratch_particle_prefab; // 할퀸 자국 폭발 이펙트 프리팹

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

    public void Scratch()
    {
        StartCoroutine(Scratching());
    }

    private IEnumerator Scratching()
    {
        int scratch_count = 3; // 총 3번의 할퀸 자국을 생성

        for (int i = 0; i < scratch_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);
            
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

using UnityEngine;

public class BossStomping : MonoBehaviour
{
    public float min_attack_distance = 6f; // 플레이어와의 최소 공격 거리
    public float impact_force = 10f; // 충격파 세기
    public GameObject impact_wave_prefab; // 충격파 프리팹
    public int wave_count = 10; // 생성할 충격파 개수
    public float damage_amount = 10f; // 플레이어에게 입힐 데미지

    private Transform player_transform; // 플레이어의 Transform
    private PlayerHealth player_health; // PlayerHealth 스크립트 참조

    void Start()
    {
        // 플레이어의 Transform 가져오기
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        // PlayerScript 참조
        player_health = player_transform.GetComponent<PlayerHealth>();

        // 일정 시간 간격으로 AttackPlayer 함수 호출
        InvokeRepeating("attackPlayer", 0f, 10f);
    }

    void attackPlayer()
    {
        // 플레이어와의 거리 계산
        float distance_to_player = Vector2.Distance(transform.position, player_transform.position);

        // 플레이어와의 거리가 최소 공격 거리 이상이고, 공격 가능 거리 이내라면
        if (distance_to_player > min_attack_distance)
        {
            // 플레이어 방향으로 충격파 생성
            Vector2 direction = (player_transform.position - transform.position).normalized;

            for (int i = 0; i < wave_count; i++)
            {
                // 충격파 생성
                GameObject impact_wave = Instantiate(impact_wave_prefab, transform.position, Quaternion.identity);

                // 충격파에 방향과 세기 설정
                impact_wave.GetComponent<Rigidbody2D>().AddForce(direction * impact_force, ForceMode2D.Impulse);

                // 플레이어에게 데미지 주기
                player_health.Damage(damage_amount);

                // 충돌 처리
                Destroy(gameObject);

            }
        }
    }
}

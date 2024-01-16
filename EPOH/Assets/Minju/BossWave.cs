using UnityEngine;

public class BossWave : MonoBehaviour
{
    public GameObject wave_prefab; // 충격파 프리팹
    public int wave_count = 10; // 생성할 충격파 개수
    public float wave_delay = 10f; // 충격파 생성 간격
    public float wave_speed = 2f; // 충격파의 이동 속도
    public float damage_amount = 10f; // 플레이어에게 입힐 데미지

    private bool is_moving_right = true; // 충격파의 초기 이동 방향

    void Start()
    {
        // 일정 시간 간격으로 WaveSpawner 함수 호출
        InvokeRepeating("waveSpawner", 0f, wave_delay);
    }

    void waveSpawner()
    {
        for (int i = 0; i < wave_count; i++)
        {
            // 충격파 생성
            GameObject impact_wave = Instantiate(wave_prefab, transform.position, Quaternion.identity);

            // 충격파에 이동 방향과 속도 설정
            impact_wave.GetComponent<Rigidbody2D>().velocity = new Vector2(is_moving_right ? wave_speed : -wave_speed, 0f);

            // 설정한 데미지를 BossWaveCollision 스크립트로 전달
            impact_wave.GetComponent<BossStomping>().damage_amount = damage_amount;
        }

        // 이동 방향 반전
        is_moving_right = !is_moving_right;
    }
}

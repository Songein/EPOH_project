using UnityEngine;
using System.Collections;

public class BossStomping : MonoBehaviour
{
    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f;
    public float start_stomping_range = 10f;

    private bool is_stomping = false;
    public float distance_to_player;

    public GameObject shockwave_prefab; // Assign the prefab in the Inspector

    private GameObject shockwave;

    private bool has_created_shockwave = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_controller = player.GetComponent<PlayerController>();

        if (player == null)
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (player != null && player_controller != null)
        {
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);

            if (!is_stomping && distance_to_player >= start_stomping_range && !has_created_shockwave)
            {
                StartCoroutine(startStompingWithDelay());
                has_created_shockwave = true; // 충격파 생성 여부를 기록
            }
            else if (is_stomping && shockwave != null)
            {
                // 플레이어와 충격파 충돌
                if (Vector3.Distance(player.transform.position, shockwave.transform.position) <= 1.2f)
                {
                    // 플레이어에게 데미지 전달
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                    }
                    stopStomping();
                }
            }
        }
        // 추가: 보스 이동 로직
        if (is_stomping && shockwave != null)
        {
            // 충격파를 플레이어 방향으로 이동
            Vector3 direction = (player.transform.position - shockwave.transform.position).normalized;
            shockwave.transform.Translate(direction * Time.deltaTime * movement_speed);
        }
    }

     IEnumerator startStompingWithDelay()
    {
        yield return new WaitForSeconds(5f); // 전조 행동을 위한 5초 대기시간
        startStomping();
    }

    void startStomping()
    {
        is_stomping = true;

        // 충격파 생성
        if (shockwave_prefab != null)
        {
            shockwave = Instantiate(shockwave_prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("shockwave prefab이 지정되지 않았습니다!");
        }
    }

    void stopStomping()
    {
        is_stomping = false;

        // 충격파 제거
        if (shockwave != null)
        {
            Destroy(shockwave);
        }
    }
}
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

    private Vector3 player_initial_position; // Added to store the player's initial position

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_controller = player.GetComponent<PlayerController>();

        if (player == null)
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다!");
        }

        // Make the boss object a trigger
        GetComponent<Collider2D>().isTrigger = true;

        // Make the boss object kinematic
        GetComponent<Rigidbody2D>().isKinematic = true;

        // Disable gravity for the boss object
        GetComponent<Rigidbody2D>().gravityScale = 0;
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
            // Move towards the player's initial position
            Vector3 direction = (player_initial_position - shockwave.transform.position).normalized;
            
            // Restrict movement along the x-axis
            direction.y = 0f;
            direction.z = 0f;
            
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

        // Store the player's initial position
        if (player != null)
        {
            player_initial_position = player.transform.position;
        }

        // 충격파 생성
        if (shockwave_prefab != null)
        {
            shockwave = Instantiate(shockwave_prefab, transform.position, Quaternion.identity);

            // Make the shockwave object kinematic
            shockwave.GetComponent<Rigidbody2D>().isKinematic = true;
        
            // Disable gravity for the shockwave object
            shockwave.GetComponent<Rigidbody2D>().gravityScale = 0;
            

        
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
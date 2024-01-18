using UnityEngine;
using System.Collections;

public class BossRunning : MonoBehaviour
{
    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f;
    public float start_chasing_range = 6f;

    private bool is_chasing = false;
    public float distance_to_player;
    private float time_since_last_player_move = 0f; // New variable to track time since the player's last movement
    public float delay_before_chasing = 5f; // 플레이어 위치 이동 후 플레이어 쫓기 전 보스 딜레이 시간

    public GameObject damage_effect_prefab;
    public float effect_duration = 1.0f;

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

            if (!is_chasing && distance_to_player >= start_chasing_range)
            {
                startChasing();
            }
            else if(is_chasing && distance_to_player > 0)
            {
                continueChasing();
                if(distance_to_player <= 0)
                {
                    // 플레이어에게 데미지 전달
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                        createDamageEffect(); //데미지 시각효과
                    }
                    stopChasing();
                }
            }
            
            // Update the time since the player's last movement
            time_since_last_player_move += Time.deltaTime;

            
        }    
    }

    void startChasing()
    {
        // Check if enough time has passed since the player's last movement
        if (time_since_last_player_move >= delay_before_chasing)
        {
            is_chasing = true;
        }
    }

    void continueChasing()
    {
        StartCoroutine(chaseWithDelay());
    }

    IEnumerator chaseWithDelay()
    {
        yield return new WaitForSeconds(5f); // 전조 행동을 위한 5초 대기시간
        Vector3 direction_to_player = (player.transform.position - transform.position).normalized;
        //transform.Translate(direction_to_player * movement_speed * Time.deltaTime);

        // Move only along the x-axis
        transform.Translate(new Vector3(direction_to_player.x, 0, 0) * movement_speed * Time.deltaTime);
    }
    
    
    
    void stopChasing()
    {
        is_chasing = false;
    }

    void createDamageEffect()
    {
        if (damage_effect_prefab != null)
        {
            GameObject damage_effect = Instantiate(damage_effect_prefab, player.transform.position, Quaternion.identity);
            
            Rigidbody2D rb = damage_effect.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            
            Destroy(damage_effect, effect_duration);
        }
    }
}

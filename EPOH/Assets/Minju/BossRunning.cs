using UnityEngine;
using System.Collections;

public class BossRunning : MonoBehaviour
{
    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f;
    public float start_chasing_range = 30f;

    private bool is_chasing = false;
    public float distance_to_player;

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
                if(distance_to_player <= 1.2)
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
            
        }    
    }

    void startChasing()
    {
        is_chasing = true;
    }

    void continueChasing()
    {
        StartCoroutine(chaseWithDelay());
    }

    IEnumerator chaseWithDelay()
    {
        yield return new WaitForSeconds(5f); // 전조 행동을 위한 5초 대기시간
        Vector3 direction_to_player = (player.transform.position - transform.position).normalized;
        transform.Translate(direction_to_player * movement_speed * Time.deltaTime);
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
            
            Rigidbody rb = damage_effect.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            
            Destroy(damage_effect, effect_duration);
        }
    }
}

using UnityEngine;
using System.Collections;

public class BossRunning : MonoBehaviour
{
    [SerializeField] float precursor_time = 1.5f; //전조 시간
    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f; // 이동 속도
    public float start_chasing_range = 6f; // 플레이어를 쫓기 시작할 거리

    private bool is_chasing = false; // 플레이어를 쫓는 중인지 여부
    public float distance_to_player; // 보스와 플레이어 간의 거리
    private float time_since_last_player_move = 0f; // 플레이어의 마지막 움직임 기록
    public float delay_before_chasing = 5f; // 플레이어 위치 이동 후 플레이어 쫓기 전 보스 딜레이 시간

    private Vector3 player_initial_position; // 플레이어의 초기 위치

    public GameObject damage_effect_prefab; // 데미지 효과 프리팹
    public float effect_duration = 1.0f; // 데미지 효과의 지속 시간

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
        // player 및 player_controller가 초기화되어 있는지 확인
        if (player != null && player_controller != null)
        {
            // 보스와 플레이어 간의 거리 계산
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);


            // 플레이어를 쫓는 상태가 아니고, 플레이어와의 거리가 지정한 범위보다 큰 경우
            if (!is_chasing && distance_to_player >= start_chasing_range)
            {
                //쫓기 시작
                startChasing();
            }

            // 플레이어를 쫓는 상태이고, 플레이어와의 거리가 0 이상인 경우
            else if(is_chasing && distance_to_player > 0)
            {
                // 계속해서 플레이어를 쫓음
                continueChasing();

                 // 플레이어와의 거리가 1.2 이하인 경우
                if(distance_to_player <= 1.2f)
                {
                    // 플레이어에게 데미지 전달, 데미지 효과 생성
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                        createDamageEffect(); //데미지 시각효과
                    }
                    // 쫓기 중지
                    stopChasing();
                }
            }
            
            // 플레이어의 마지막 움직임 시간 업데이트
            time_since_last_player_move += Time.deltaTime;

            
        }    
    }

    void startChasing()
    {
        // Check if enough time has passed since the player's last movement
        if (time_since_last_player_move >= delay_before_chasing)
        {
            is_chasing = true;
            

            // 쫓기 시작할 때 플레이어의 초기 위치 기록
            if (player != null)
            {
                player_initial_position = player.transform.position;
            }
        }
    }

    void continueChasing()
    {
        // 일정 시간 후에 쫓기 시작
        StartCoroutine(chaseWithDelay());
    }

    IEnumerator chaseWithDelay()
    {
        yield return new WaitForSeconds(5f); // 전조 행동을 위한 5초 대기시간

         // 플레이어의 초기 위치를 향해 이동하는 벡터 계산
        Vector3 direction_to_player = (player_initial_position - transform.position).normalized;

        // x 축 방향으로만 이동
        transform.Translate(new Vector3(direction_to_player.x, 0, 0) * movement_speed * Time.deltaTime);
    }
    
    
    
    void stopChasing()
    {
        // 쫓기 상태 비활성화
        is_chasing = false;
    }

    //데미지 효과 생성
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
    

    IEnumerator StartRunning()
    {
        Debug.Log("[Running] : 보스가 몸을 웅크림.");
        //is_attacking = true;
        yield return new WaitForSeconds(precursor_time);

        // player 및 player_controller가 초기화되어 있는지 확인
        if (player != null && player_controller != null)
        {
            // 보스와 플레이어 간의 거리 계산
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);

            // 플레이어를 쫓는 상태가 아니고, 플레이어와의 거리가 지정한 범위보다 큰 경우
            if (!is_chasing && distance_to_player >= start_chasing_range)
            {
                //쫓기 시작
                // Check if enough time has passed since the player's last movement
                if (time_since_last_player_move >= delay_before_chasing)
                {
                    is_chasing = true;
                    

                    // 쫓기 시작할 때 플레이어의 초기 위치 기록
                    if (player != null)
                    {
                        player_initial_position = player.transform.position;
                    }
                }
            }

            // 플레이어를 쫓는 상태이고, 플레이어와의 거리가 0 이상인 경우
            else if(is_chasing && distance_to_player > 0)
            {
                // 계속해서 플레이어를 쫓음
                yield return new WaitForSeconds(precursor_time); // 전조시간동안 대기

                // 플레이어의 초기 위치를 향해 이동하는 벡터 계산
                Vector3 direction_to_player = (player_initial_position - transform.position).normalized;

                // x 축 방향으로만 이동
                transform.Translate(new Vector3(direction_to_player.x, 0, 0) * movement_speed * Time.deltaTime);

                // 플레이어와의 거리가 1.2 이하인 경우
                if(distance_to_player <= 1.2f)
                {
                    // 플레이어에게 데미지 전달, 데미지 효과 생성
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                    }
                    // 쫓기 상태 비활성화
                    is_chasing = false;
                }
                
            }
            // 플레이어의 마지막 움직임 시간 업데이트
            time_since_last_player_move += Time.deltaTime;

        }
    }
}

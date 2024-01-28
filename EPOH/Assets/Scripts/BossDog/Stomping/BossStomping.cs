using UnityEngine;
using System.Collections;

public class BossStomping : MonoBehaviour
{
    [SerializeField] float precursor_time = 1.5f; //전조 시간

    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f; // 이동 속도
    public float start_stomping_range = 10f; // 발구르기 시작 거리

    private bool is_stomping = false; // 발구르기 중인지 여부
    public float distance_to_player; // 보스와 플레이어 간의 거리

    public GameObject shockwave_prefab; // 충격파 prefab
    private GameObject shockwave; // 생성된 충격파 오브젝트에 대한 참조

    private bool has_created_shockwave = false; // 충격파 생성 여부

    private Vector3 player_initial_position; // 플레이어의 초기 위치 저장

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_controller = player.GetComponent<PlayerController>();

        // 플레이어를 찾지 못한 경우 오류를 표시
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

    /*
    void Update()
    {
        // player 및 player_controller가 초기화되어 있는지 확인
        if (player != null && player_controller != null)
        {
            // 보스와 플레이어 간의 거리 계산
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);

            // 발구르기 중이 아니고, 플레이어와의 거리가 지정한 범위보다 크고 충격파가 생성되지 않은 경우
            if (!is_stomping && distance_to_player >= start_stomping_range && !has_created_shockwave)
            {
                // 일정 시간 후 발구르기 시작
                StartCoroutine(startStompingWithDelay());
                // 충격파 생성 여부를 기록
                has_created_shockwave = true; 
            }
            //발구르기 중이고 충격파 오브젝트가 존재하는 경우
            else if (is_stomping && shockwave != null)
            {
                // 플레이어와 충격파 충돌
                if (Vector3.Distance(player.transform.position, shockwave.transform.position) <= 1.2f)
                {
                    // 플레이어에게 데미지 전달
                    //PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    //if (player_health != null)
                    //{
                    //    player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                    //}
                    

                    
                    

                    //발구르기 중지
                    stopStomping();
                }
            }
        }
        
        // 보스가 발구르기 중이면서 충격파 오브젝트가 존재하는 경우
        if (is_stomping && shockwave != null)
        {
            // 충돌 감지를 Collider2D를 이용하여 수행
            Collider2D player_collider = player.GetComponent<Collider2D>();
            Collider2D shockwave_collider = shockwave.GetComponent<Collider2D>();


            //충돌 감지에 필요한 컴포넌트가 존재하는 경우
            if (player_collider != null && shockwave_collider != null)
            {
                // 충돌 검사
                if (Physics2D.IsTouching(player_collider, shockwave_collider))
                {
                    // 충돌 발생: 플레이어에게 데미지 전달
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f);
                        //발구르기 중지
                        stopStomping();
                    }
                }
            }

            // 플레이어의 초기 위치를 향해 이동하는 벡터 계산
            Vector3 direction = (player_initial_position - shockwave.transform.position).normalized;
            
            // y 및 z 축 방향 이동 제한
            direction.y = 0f;
            direction.z = 0f;
            
            shockwave.transform.Translate(direction * Time.deltaTime * movement_speed);
        }
    }


    //일정 시간 후 발구르기 시작하는 코루틴
    IEnumerator startStompingWithDelay()
    {
        yield return new WaitForSeconds(5f); // 전조 행동을 위한 5초 대기시간
        
        startStomping();
    }

    void startStomping() //발구르기 시작
    {
        //발구르기 중인 상태로 변경
        is_stomping = true;

        // 플레이어의 초기 위치 저장
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



    void stopStomping() // 발구르기 중지
    {
        // 발구르기 상태 비활성화
        is_stomping = false;

        // 충격파 제거
        if (shockwave != null)
        {
            Destroy(shockwave);
        }
    }
    */


    IEnumerator startStomping()
    {
        Debug.Log("[Stomping] : 보스가 앞발을 든다.");
        //is_attacking = true;
        yield return new WaitForSeconds(precursor_time);

        // player 및 player_controller가 초기화되어 있는지 확인
        if (player != null && player_controller != null)
        {
            // 보스와 플레이어 간의 거리 계산
            distance_to_player = Vector3.Distance(transform.position, player.transform.position);

            // 발구르기 중이 아니고, 플레이어와의 거리가 지정한 범위보다 크고 충격파가 생성되지 않은 경우
            if (!is_stomping && distance_to_player >= start_stomping_range && !has_created_shockwave)
            {
                // 일정 시간 후 발구르기 시작
                yield return new WaitForSeconds(precursor_time); // 전조 행동을 위한 5초 대기시간
                //발구르기 시작
                //발구르기 중인 상태로 변경
                is_stomping = true;

                // 플레이어의 초기 위치 저장
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

                // 충격파 생성 여부를 기록
                has_created_shockwave = true; 
            }
            //발구르기 중이고 충격파 오브젝트가 존재하는 경우
            else if (is_stomping && shockwave != null)
            {
                // 플레이어와 충격파 충돌
                if (Vector3.Distance(player.transform.position, shockwave.transform.position) <= 1.2f)
                {
                    // 플레이어에게 데미지 전달
                    /*PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f); // 플레이어에게 데미지 10을 입힘
                    }
                    */



                    //발구르기 중지
                    // 발구르기 상태 비활성화
                    is_stomping = false;

                    // 충격파 제거
                    if (shockwave != null)
                    {
                        Destroy(shockwave);
                    }
                }
            }
        }
        
        // 보스가 발구르기 중이면서 충격파 오브젝트가 존재하는 경우
        if (is_stomping && shockwave != null)
        {
            // 충돌 감지를 Collider2D를 이용하여 수행
            Collider2D player_collider = player.GetComponent<Collider2D>();
            Collider2D shockwave_collider = shockwave.GetComponent<Collider2D>();


            //충돌 감지에 필요한 컴포넌트가 존재하는 경우
            if (player_collider != null && shockwave_collider != null)
            {
                // 충돌 검사
                if (Physics2D.IsTouching(player_collider, shockwave_collider))
                {
                    // 충돌 발생: 플레이어에게 데미지 전달
                    PlayerHealth player_health = player.GetComponent<PlayerHealth>();
                    if (player_health != null)
                    {
                        player_health.Damage(10f);
                        //발구르기 중지
                        // 발구르기 상태 비활성화
                        is_stomping = false;

                        // 충격파 제거
                        if (shockwave != null)
                        {
                            Destroy(shockwave);
                        }
                    }
                }
            }

            // 플레이어의 초기 위치를 향해 이동하는 벡터 계산
            Vector3 direction = (player_initial_position - shockwave.transform.position).normalized;
            
            // y 및 z 축 방향 이동 제한
            direction.y = 0f;
            direction.z = 0f;
            
            shockwave.transform.Translate(direction * Time.deltaTime * movement_speed);
        }

    }
}
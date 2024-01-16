using UnityEngine;

public class BossRunning : MonoBehaviour
{
    private GameObject player;
    private PlayerController player_controller;
    public float movement_speed = 12f;
    public float start_chasing_range = 10f;

    private bool isChasing = false;
    public float distance_to_player;

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

            if (!isChasing && distance_to_player >= start_chasing_range)
            {
                StartChasing();
            }
            else if(isChasing && distance_to_player > 0)
            {
                ContinueChasing();
                if(distance_to_player == 0)
                {
                    StopChasing();
                }
            }  
            else if(isChasing && distance_to_player == 0)  
            {
                StopChasing();
            }
        }    
    }

    void StartChasing()
    {
        isChasing = true;
    }

    void ContinueChasing()
    {
        Vector3 direction_to_player = (player.transform.position - transform.position).normalized;
        transform.Translate(direction_to_player * movement_speed * Time.deltaTime);
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 부딪혔을 때 처리
            Debug.Log("플레이어와 부딪혔습니다!");
            
            // 플레이어에게 데미지 전달
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(10f); // 10만큼의 데미지를 입힘
            }


            // 플레이어 오브젝트에 Rigidbody2D가 있는지 확인
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // 플레이어를 밀어내지 않도록 velocity를 0으로 설정
                playerRigidbody.velocity = Vector2.zero;
            }
            StopChasing();
        }
    }

    void StopChasing()
    {
        isChasing = false;
    }
}

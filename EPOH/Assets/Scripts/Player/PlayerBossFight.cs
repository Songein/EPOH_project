using UnityEngine;

public class PlayerBossFight : MonoBehaviour
{
    private PlayerController player_controller;
    private BossManager boss_manager;

    void Start()
    {
        player_controller = GetComponent<PlayerController>();
        boss_manager = FindObjectOfType<BossManager>();

        if (player_controller == null)
        {
            Debug.LogError("PlayerController 스크립트를 찾을 수 없습니다.");
        }

        if (boss_manager == null)
        {
            Debug.LogError("BossManager 스크립트를 찾을 수 없습니다.");
        }
    }

    void Attack()
    {
        if (!player_controller.is_attacking)
        {
            player_controller.is_attacking = true;

            boss_manager.boss_hp -= 20; // 플레이어의 공격을 받으면 boss_hp 값 -20
            boss_manager.hacking_point += 20; // Boss에게 공격 성공시 hacking_point +20

            //Bossmanager의 boss_hp 값이 0이 되면 더이상 감소하지 않음
            if (boss_manager.boss_hp <= 0)
            {
                boss_manager.boss_hp = 0;
                GameManager.instance.boss_clear_info[GameManager.instance.boss_num] = true;
            }
        }
    }

    void Update()
    {

        if (Input.GetButtonDown("Teleport"))
        {
            if (player_controller.can_teleport)
            {
                StartCoroutine(player_controller.Teleport());

                // Player가 순간이동 시 hacking_point -10
                boss_manager.hacking_point -= 10;
            }
        }

        // hacking_point가 200 달성시 임무완료 씬으로 이동
        if (boss_manager.hacking_point >= 200)
        {
            
        }
    }
    

    // Additional method to handle boss movement and properties
    void HandleBossMovement()
    {
        // Assuming you have a reference to the boss object
        GameObject bossObject = GameObject.Find("Boss"); // Change "Boss" to the actual name of your boss object

        if (bossObject != null)
        {
            Rigidbody2D bossRigidbody = bossObject.GetComponent<Rigidbody2D>();
            
            if (bossRigidbody != null)
            {
                // Set the boss object to be a trigger and kinematic
                bossRigidbody.isKinematic = true;
                bossRigidbody.gravityScale = 0f;

                // Make the boss move only along the x-axis
                bossRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            Debug.LogError("Boss 오브젝트를 찾을 수 없습니다.");
        }
    }
}
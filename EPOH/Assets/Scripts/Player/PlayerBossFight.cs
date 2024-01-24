using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBossFight : MonoBehaviour
{
    private PlayerController player_controller;
    private BossManager boss_manager;
    private AttackArea attack_area;
    private PlayerAttack player_attack;

    private GameObject player;
    private GameObject boss;
    private GameObject attack_area_object;

    private int combo_count = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        boss = GameObject.FindWithTag("Boss");

        player_controller = player.GetComponent<PlayerController>();
        boss_manager = boss.GetComponent<BossManager>();

        player_attack = player.GetComponent<PlayerAttack>();  

        
        if (player_controller == null)
        {
            Debug.LogError("PlayerController 스크립트를 찾을 수 없습니다.");
        }

        if (boss_manager == null)
        {
            Debug.LogError("BossManager 스크립트를 찾을 수 없습니다.");
        }

        if (player_attack == null)
        {
            Debug.LogError("PlayerAttack 스크립트를 찾을 수 없습니다.");
        }
        else{
            attack_area_object = player.transform.Find("AttackArea").gameObject;
            attack_area = attack_area_object.GetComponentInChildren<AttackArea>();

            if (attack_area == null)
            {
                Debug.LogError("AttackArea 스크립트를 찾을 수 없습니다.");
            }

        }

        
        
        if (player == null)
        {
            Debug.LogError("Player GameObject를 찾을 수 없습니다. PlayerBossFight 스크립트에 Player GameObject를 할당해주세요.");
        }
    }

    void attackBoss()
    {
        if (!player_controller.is_attacking)
        {
            player_controller.is_attacking = true;
            comboCount(); // ComboCount 메서드 호출

            if (player_attack != null && attack_area != null)
            {

                // 현재 콤보에 해당하는 공격 세기 설정
                float current_attack_power = player_attack.combo_attack_power[Mathf.Clamp(combo_count - 1, 0, player_attack.combo_attack_power.Length - 1)];

                // 공격 범위의 공격 세기 설정
                attack_area.SetAttackPower(current_attack_power);

                // Check if the attack area collides with the boss
                Collider2D boss_collider = boss.GetComponent<Collider2D>();
                if (boss_collider != null && attack_area.GetComponent<Collider2D>().IsTouching(boss_collider))
                {
                    Debug.Log("Boss를 공격했습니다!");

                    boss_manager.boss_hp -= current_attack_power; // 플레이어의 공격을 받으면 boss_hp 값 공격세기만큼 감소
                    if(boss_manager.boss_hp > 0)
                    {
                        Debug.Log("boss_hp 값: " + boss_manager.boss_hp);
                    }
                    //Bossmanager의 boss_hp 값이 0이 되면 더이상 감소하지 않음
                    else if(boss_manager.boss_hp <= 0)
                    {
                        boss_manager.boss_hp = 0;
                        Debug.Log("boss_hp 값: " + boss_manager.boss_hp);
                    }
                    

                    boss_manager.hacking_point += 1; // Boss에게 공격 성공시 hacking_point 값 +1
                    if(boss_manager.hacking_point != 200)
                    {
                        Debug.Log("hacking_point 값: " + boss_manager.hacking_point);
                    }
                    else if (boss_manager.hacking_point == 200)
                    {
                        Debug.Log("hacking_point 값: " + boss_manager.hacking_point);   
                    }
                }
                
            }

            player_controller.is_attacking = false;
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

        // hacking_point= 200 이 되고 boss_hp = 0 이 되면 임무완료 씬으로 이동
        if (boss_manager != null && boss_manager.hacking_point >= 200 && boss_manager.boss_hp == 0)
        {
            SceneManager.LoadScene("MissionClear");
            //GameManager.instance.boss_clear_info[GameManager.instance.boss_num] = true;
        }

        // 공격 버튼 처리
        if (Input.GetButtonDown("Attack") && !player_controller.is_attacking)
        {
            comboCount();
            attackBoss();
        }
    }
    

    // Additional method to handle boss movement and properties
    void handleBossMovement()
    {
        if (boss != null)
        {
            Rigidbody2D boss_rigidbody = boss.GetComponent<Rigidbody2D>();
            
            if (boss_rigidbody != null)
            {
                // Set the boss object to be a trigger and kinematic
                boss_rigidbody.isKinematic = true;
                boss_rigidbody.gravityScale = 0f;

                // Make the boss move only along the x-axis
                boss_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            Debug.LogError("Boss 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void comboCount()
    {
        combo_count++;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth :MonoBehaviour
{
    public BossManager boss_manager;
    public Hacking hacking;
    private SpriteRenderer sr; //보스의 SpriteRenderer 참조
    private Animator animator;

    private GameObject boss;
    public float boss_hp = 1000f; //보스의 목숨

    private Vector2 attack_direction;
    [SerializeField] private float knockback_speed;
    

    public void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        boss_manager = boss.GetComponent<BossManager>();
        hacking = GetComponent<Hacking>();
        sr = GetComponent<SpriteRenderer>(); //보스의 SpriteRenderer 할당
        animator = GetComponent<Animator>();
    }
    
    public void Damage(float power)
    {
        //피격 애니 실행
        CheckAttackDirection(FindObjectOfType<PlayerController>().transform.position);
        //animator.SetTrigger("IsHit");
        sr.color = new Color(1, 0, 0, 0.6f); // 보스의 스프라이트 색을 투명한 빨강색으로 변경
        Invoke("ReturnColor",1f);
        //KnockBack
        transform.position = Vector2.Lerp(transform.position, attack_direction, 5 * Time.deltaTime);
        // Ensure boss_hp doesn't go below 0
        if (boss_hp > 0)
        {
        
            boss_hp -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
            
            if (boss_hp <= 0)
            {
                boss_hp = 0;
                boss_manager.boss_hp = boss_hp;
                hacking.checkHackingPoint();
            }
            else
            { 
                Debug.Log("[Enemy] 남은 목숨 : " + boss_hp);
                boss_manager.boss_hp = boss_hp;

            }
        }
    }

    public void Die()
    {
        Debug.Log("[Enemy] : " + gameObject.name + " 사망");
        //gameObject.SetActive(false);
    }

    void ReturnColor()
    {
        //보스의 스프라이트 색 원래대로 회복
        sr.color = new Color(1, 1, 1, 1f);
    }
    
    public void CheckAttackDirection(Vector3 player)
    {
        Debug.Log(transform.position.x - player.x);
        //공격 당시 보스의 위치 - 플레이어 위치가 양수면 보스는 오른쪽에 위치함
        if (transform.position.x - player.x >= 0)
        {
            float pos = transform.position.x + 1f * knockback_speed;
            if (pos >= 13f)
            {
                pos = 13f;
            }
            attack_direction = new Vector2(pos, -3f);
            Debug.Log("공격방향 왼쪽");
        }
        else
        {
            float pos = transform.position.x + (-1f) * knockback_speed;
            
            if (pos <= -15.5f)
            {
                pos = 15.5f;
            }
            attack_direction = new Vector2(pos, -3f);
            Debug.Log("공격방향 오른쪽");
        }

    }
}

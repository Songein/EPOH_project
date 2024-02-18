using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public BossManager boss_manager; // BossManager 스크립트 참조
    private Animator animator; //player Animator 참조
    private PlayerController pc; //PlayerController 참조

    public float player_hp = 200; //플레이어의 목숨
    private SpriteRenderer sp; //플레이어 SpriteRenderer 참조
    private bool is_invincible; //무적 여부

    private Vector2 attack_direction; //공격 방향
    [SerializeField] private float knockback_speed; //knockback speed
    
    void Start()
    {
        //SpriteRenderer 할당하기
        sp = GetComponent<SpriteRenderer>();

        boss_manager = FindObjectOfType<BossManager>();

        //player Animator 할당
        animator = GetComponent<Animator>();
        //PlayerController 할당
        pc = GetComponent<PlayerController>();

    }
    
    //플레이어 데미지 관련
    public void Damage(float power)
    {
        //무적시간이 아닌 경우
        if (!is_invincible)
        {
            Debug.Log("[PlayerHealth] : 플레이어가 공격 받음");
            player_hp -= power; //파라미터로 입력받은 power 만큼 hp가 감소함.
            Debug.Log("[PlayerHealth] : 남은 hp " + player_hp);

            if (player_hp <= 0) //플레이어 목숨이 0이하라면
            {
                player_hp = 0;
                boss_manager.player_hp = player_hp; // BossManager의 player_hp 값 업데이트
                Die();
            }
            else
            {
                StartCoroutine(Invincible()); //무적시간 호출

                boss_manager.player_hp = player_hp; // BossManager의 player_hp 값 업데이트
            }  
        }
    }
    //플레이어 무적시간
    private IEnumerator Invincible()
    {
        //오브젝트의 레이어를 PlayerDamaged로 변경
        //gameObject.layer = 8;
        //피격 애니메이션 실행
        animator.SetTrigger("IsHit");
        
        //KnockBack
        transform.position = Vector2.Lerp(transform.position, attack_direction, 5 * Time.deltaTime);
        
        
        //무적 여부를 담고 있는 is_invincible 변수를 true로 변경
        is_invincible = true;
        //오브젝트의 색 변경(하얀 투명색)
        //sp.color = new Color(1, 1, 1, 0.6f);

        yield return new WaitForSeconds(2f); //무적시간 2초
        //오브젝트의 레이어를 Player로 변경
        //gameObject.layer = 6;
        //무적 여부를 담고 있는 is_invincible 변수를 true로 변경
        is_invincible = false;
        //오브젝트의 색 원래대로 변경
        //sp.color = new Color(1, 1, 1, 1f);
    }

    //플레이어 사망
    public void Die()
    {
        Debug.Log("[PlayerHealth] : 플레이어 사망");
        //gameObject.SetActive(false); //플레이어 오브젝트 비활성화
    }

    public void CheckAttackDirection(Vector3 boss)
    {
        Debug.Log(boss.x - transform.position.x);
        //공격 당시 보스의 위치 - 플레이어 위치가 양수면 공격방향은 왼쪽임
        if (boss.x - transform.position.x >= 0)
        {
            attack_direction = new Vector2(transform.position.x + -1f * knockback_speed, 1f);
            Debug.Log("공격방향 왼쪽");
        }
        else
        {
            attack_direction = new Vector2(transform.position.x + 1f * knockback_speed, 1f);
            Debug.Log("공격방향 오른쪽");
        }

    }
}

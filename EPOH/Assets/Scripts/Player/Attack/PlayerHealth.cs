using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public BossManager boss_manager;

    public float player_hp = 200; //플레이어의 목숨
    public bool is_invincible; //무적 여부
    
    private SpriteRenderer sp; //플레이어 SpriteRenderer 참조

    void Start()
    {
        //SpriteRenderer 할당하기
        sp = GetComponent<SpriteRenderer>();
        boss_manager = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossManager>();
    }
    
    //플레이어 데미지 관련
    public void Damage(float power)
    {
        //무적시간이 아닌 경우 
        if (!is_invincible)
        {
            Debug.Log("[PlayerHealth] : 플레이어가 공격 받음");

            //파라미터로 입력받은 power 만큼 hp가 감소함.
            player_hp -= power;
            boss_manager.player_hp = player_hp;
            Debug.Log("[PlayerHealth] : 남은 hp " + player_hp);

            if (player_hp <= 0) //플레이어 목숨이 0이하라면
            {
                Die();
            }
            else
            {
                StartCoroutine(Invincible()); //무적시간 호출
            }  
        }

        if (!boss_manager.battle_start && (boss_manager.phase1_start || boss_manager.phase2_start || boss_manager.phase3_start || boss_manager.phase4_start))
        {
            //오브젝트의 레이어를 Invincible로 변경
            gameObject.layer = 9;
        }
    }
    //플레이어 무적시간
    private IEnumerator Invincible()
    {
        //오브젝트의 레이어를 PlayerDamaged로 변경
        gameObject.layer = 8;
        //무적 여부를 담고 있는 is_invincible 변수를 true로 변경
        is_invincible = true;
        //오브젝트의 색 변경(하얀 투명색)
        sp.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(2f); //무적시간 2초
        //오브젝트의 레이어를 Player로 변경
        gameObject.layer = 6;
        //무적 여부를 담고 있는 is_invincible 변수를 true로 변경
        is_invincible = false;
        //오브젝트의 색 원래대로 변경
        sp.color = new Color(1, 1, 1, 1f);
    }

    //플레이어 사망
    public void Die()
    {
        Debug.Log("[PlayerHealth] : 플레이어 사망");
        gameObject.SetActive(false); //플레이어 오브젝트 비활성화
    }
    
}

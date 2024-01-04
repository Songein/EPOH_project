using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float max_combo_time = 0.25f; //콤보 공격 가능 최대 시간
    private float combo_timer = 0f; //마지막 콤보 공격으로부터의 타이머
    private int cur_combo_cnt = 1; //현재 몇 단 콤보인지
    public float[] combo_power = { 2f, 4f, 10f }; //콤보 공격 별 세기

    private Animator animator; //플레이어 애니메이터 할당 변수

    void Start()
    {
        //플레이어 애니메이터 할당
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        combo_timer += Time.deltaTime; //콤보 공격 타이머 실행;
        //공격버튼 누르기
        //콤보 공격 가능한지 판단
        //콤보 공격 단에 따라 함수 호출
        if (Input.GetKeyDown("Attack"))
        {
            //마지막 콤보 공격으로부터 콤보 공격 가능한 최대 시간이 지나지 않았으면(콤보 공격 가능하면)
            if (combo_timer <= max_combo_time)
            {
                Attack(cur_combo_cnt++);
            }
            else //콤보 공격 가능 시간이 지났으면
            {
                cur_combo_cnt = 1;
                Attack(cur_combo_cnt);
            }
            combo_timer = 0f;
        }
        
        
    }

    void Attack(int comb_cnt)
    {
        
    }
    
}

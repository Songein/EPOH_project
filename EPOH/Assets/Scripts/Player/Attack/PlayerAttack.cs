using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool is_attacking = false; //현재 공격 중인지
    public float[] combo_attack_power = { 30f, 50f, 80f }; //콤보 별 공격 세기

    public Animator animator; //플레이어 애니메이터 변수
    public static PlayerAttack instance;
    private GameObject attack_area; //공격범위 오브젝트 참조 변수

    void Start()
    {
        //플레이어 애니메이터 할당
        animator = GetComponent<Animator>();

        //공격 범위 참조 후 비활성화
        attack_area = transform.GetChild(0).gameObject;
        attack_area.SetActive(is_attacking);
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //공격 버튼을 누르고 공격 중이지 않으면
        if (Input.GetButtonDown("Attack") && !is_attacking)
        {
            //공격 함수 호출
            Attack();
        }
        
    }

    //공격 함수
    void Attack()
    {
        is_attacking = true;
    }
    
}

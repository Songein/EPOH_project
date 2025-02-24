using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //인스턴스화
    public static PlayerAttack instance;
    
    public bool is_attacking = false; //현재 공격 중인지
    public float[] combo_attack_power = { 30f, 50f}; //콤보 별 공격 세기
    public GameObject attack_area; //공격범위 오브젝트 참조 변수
    
    void Awake()
    {
        instance = this;
        attack_area = transform.GetChild(0).GetChild(0).gameObject;
        //공격 범위 참조 후 비활성화
        transform.GetChild(0).gameObject.SetActive(is_attacking);
    }

    void Update()
    {
        //공격 버튼을 누르고 공격 중이지 않으면
        if (Input.GetButtonDown("Attack") && !is_attacking && !PlayerInteract.instance.is_interacting && !PlayerInteract.instance.is_talking)
        {
            //공격 함수 호출
            Attack();
        }
        
    }

    //공격 함수
    void Attack()
    {
        is_attacking = true;
        transform.GetComponent<Animator>().Play("Attack One");
        SoundManager2.instance.PlayAttack();
    }
    
}

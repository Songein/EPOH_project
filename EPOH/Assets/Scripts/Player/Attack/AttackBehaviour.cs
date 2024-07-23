using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    //콤보(2단) 공격 실행 여부
    private bool isComboAttack = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Attack Area 오브젝트 찾기(비활성화 오브젝트이기에 부모 오브젝트인 Player 오브젝트를 통해 접근
        GameObject attack_area = animator.transform.Find("AttackArea").gameObject;
        
        //공격 상태 진입하면 Attac Area 오브젝트 활성화
        attack_area.SetActive(true);
        //공격 상태 true로 변경
        PlayerAttack.instance.is_attacking = true;
        //콤보 공격 여부 false로 초기화
        isComboAttack = false;
        
        //공격 상태 종류에 따라 공격 세기 설정
        if (stateInfo.IsName("Attack One"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[0]);
        }
        else if (stateInfo.IsName("Attack Second"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[1]);
        }
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //1단 공격 중에 공격 버튼을 누르면 2단 공격 진행
        if (stateInfo.IsName("Attack One") && Input.GetButtonDown("Attack") && !isComboAttack)
        {
            isComboAttack = true;
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //공격 범위 오브젝트 비활성화
        animator.transform.Find("AttackArea").gameObject.SetActive(false);

        if (isComboAttack)
        {
            //콤보 공격이 가능하다면
            animator.Play("Attack Two");
        }
        else
        {
            //콤보 공격이 아니라면 1단 공격으로 마무리
            //공격 상태를 false로 변경
            PlayerAttack.instance.is_attacking = false;
        }
        
    }

}

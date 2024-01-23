using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PlayerController 컴포넌트 할당
        PlayerController pr = FindObjectOfType<PlayerController>();

        // PlayerController를 찾지 못한 경우 예외 처리
        if (pr == null)
        {
            Debug.LogError("PlayerController를 찾을 수 없습니다.");
            return;
        }

        //플레이어가 상호작용 중이거나 대화 중이면 공격 불가능하도록 조건 추가
        if (PlayerAttack.instance != null && PlayerAttack.instance.is_attacking && !pr.is_interacting && !pr.is_talking)
        {
            if (PlayerAttack.instance != null)
            {
            PlayerAttack.instance.animator.Play("Attack One");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttack.instance.is_attacking = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

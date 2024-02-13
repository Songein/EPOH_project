using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public AudioClip attack1SelectClip; // 공격1 선택음
    public AudioClip attack2SelectClip; // 공격2 선택음
    public AudioClip attack3SelectClip; // 공격3 선택음


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //AudioSource 공격음 
        AudioSource audioSource = animator.GetComponent<AudioSource>();

        //Attack Area 오브젝트 찾기(비활성화 오브젝트이기에 부모 오브젝트인 Player 오브젝트를 통해 접근
        GameObject attack_area = GameObject.Find("Player").transform.Find("AttackArea").gameObject;
        PlayerController pr = FindObjectOfType<PlayerController>(); //PlayerController 할당
        //공격 상태 진입하면 Attac Area 오브젝트 활성화
        attack_area.SetActive(true);
        pr.is_attacking = true; //공격 상태 true로 변경
        //공격 상태 종류에 따라 공격 세기 설정
        if (stateInfo.IsName("Attack One"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[0]);

            if (attack1SelectClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(attack1SelectClip);
            }
            else
            {
                Debug.LogWarning("attack1SelectClip이나 AudioSource가 null입니다.");
            }
            
            
        }
        else if (stateInfo.IsName("Attack Two"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[1]);
            
            if (attack2SelectClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(attack2SelectClip);
            }
            else
            {
                Debug.LogWarning("attack2SelectClip이나 AudioSource가 null입니다.");
            }
            
        }
        else if (stateInfo.IsName("Attack Three"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[2]);
            
            if (attack3SelectClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(attack3SelectClip);
            }
            else
            {
                Debug.LogWarning("attack3SelectClip이나 AudioSource가 null입니다.");
            }
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Player").transform.Find("AttackArea").gameObject.SetActive(false);  
        PlayerController pr = FindObjectOfType<PlayerController>(); //PlayerController 할당
        pr.is_attacking = false; //공격 상태 false로 변경
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

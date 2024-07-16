using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Attack Area 오브젝트 찾기(비활성화 오브젝트이기에 부모 오브젝트인 Player 오브젝트를 통해 접근
        GameObject attack_area = GameObject.Find("Player").transform.Find("AttackArea").gameObject;
        PlayerController pr = FindObjectOfType<PlayerController>(); //PlayerController 할당
        
        //공격 상태 진입하면 Attac Area 오브젝트 활성화
        attack_area.SetActive(true);
        
        //공격 상태 true로 변경
        pr.is_attacking = true;
        
        //공격 상태 종류에 따라 공격 세기 설정
        if (stateInfo.IsName("Attack One"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[0]);
        }
        else if (stateInfo.IsName("Attack Two"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[1]);
        }
        else if (stateInfo.IsName("Attack Three"))
        {
            attack_area.GetComponent<AttackArea>().SetAttackPower(PlayerAttack.instance.combo_attack_power[2]);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Player").transform.Find("AttackArea").gameObject.SetActive(false);  
        
        //PlayerController 할당
        PlayerController pr = FindObjectOfType<PlayerController>();

        //공격 상태 false로 변경
        pr.is_attacking = false; //공격 상태 false로 변경
    }

}

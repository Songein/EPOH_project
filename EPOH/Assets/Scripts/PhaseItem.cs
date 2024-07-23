using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseItem : MonoBehaviour
{
    public Hacking hacking;

    public GameObject bossPhaseItem;
    public float phase_item_hp = 30; //페이즈 목숨 30으로 설정

    // Start is called before the first frame update
    void Start()
    {
        bossPhaseItem = GameObject.FindWithTag("BossPhaseItem");
        hacking = GameObject.FindGameObjectWithTag("Player").GetComponent<Hacking>();
    }

    public void phaseItemDamage(float power)
    {
        if (phase_item_hp > 0)
        {
            phase_item_hp -= power; //파라미터로 받은 공격 세기에 따라 목숨 감소
            if (phase_item_hp <= 0)
            {
                phaseItemDie();
            }
            else
            { 
                Debug.Log("[PhaseItem] 남은 목숨 : " + phase_item_hp);

            }
        }
    }

    void phaseItemDie()
    {
        Debug.Log("[PhaseItem] : " + gameObject.name + " 사망");
        
        gameObject.SetActive(false);
    }
}

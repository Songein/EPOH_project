using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class Attackable : MonoBehaviour
{
    [SerializeField] protected float _duration;
    [SerializeField] protected float _healthDamage;
    [SerializeField] protected float _hackPointDamage;
    [SerializeField] protected bool _attackHealth = false;
    [SerializeField] protected bool _attackHackPoint = false;
    

    //플레이어와 부딪힐 경우
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (_attackHealth)
            {
                PlayerHealth playerHealth = other.transform.GetComponent<PlayerHealth>();
                playerHealth.Damage(_healthDamage);
            }

            if (_attackHackPoint)
            {
                BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(_hackPointDamage);
            }
        }
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (_attackHealth)
            {
                PlayerHealth playerHealth = other.transform.GetComponent<PlayerHealth>();
                playerHealth.Damage(_healthDamage);
            }

            if (_attackHackPoint)
            {
                BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(_hackPointDamage);
            }
        }
    }

    protected IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(_duration);
        BossManagerNew.Current.OnSkillEnd?.Invoke();
        Destroy(gameObject);
    }
}

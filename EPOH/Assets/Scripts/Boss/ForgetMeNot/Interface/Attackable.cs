using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class Attackable : MonoBehaviour
{
    [SerializeField] private float _duration;
    public float Duration
    {
        get { return _duration; }
        private set { _duration = value; }
    }
    [SerializeField] private float _damage;
    public float Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }

    void Awake()
    {
        Duration = _duration;
        Damage = _damage;
    }

    //플레이어와 부딪힐 경우
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.transform.GetComponent<PlayerHealth>();
            playerHealth.Damage(this.Damage);
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Damage(this.Damage);
        }
    }

    protected IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }
}

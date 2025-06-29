using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Hittable
{
    [SerializeField] private float _healthDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Ground"))
        {
            FindObjectOfType<PlayerHealth>().Damage(_healthDamage);
            BossManagerNew.Current.OnDecreaseHackingPoint(_hackPoint);
            
            Destroy(gameObject);
        }


    }

    public override void OnRemoved()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : Hittable
{
    [SerializeField] private float _increaseHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("Ground"))
        {
            FindObjectOfType<PlayerHealth>().IncreaseHealth(_increaseHealth);
            BossManagerNew.Instance.OnIncreaseHackingPoint(_hackPoint);
            
            Destroy(gameObject);
        }
    }

    public override void OnRemoved()
    {
        Destroy(gameObject);
    }
}

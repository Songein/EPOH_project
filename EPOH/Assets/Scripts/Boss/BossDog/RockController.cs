using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] private float damagePower;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Damage(damagePower);
            Debug.Log("돌과 플레이어 충돌");
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Debug.Log("돌과 땅 충돌");
            Destroy(this.gameObject);
        }
    }
}

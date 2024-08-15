using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionWave : MonoBehaviour
{
    //충격파 세기
    [SerializeField] float power = 10f;
    void OnTriggerEnter2D(Collider2D other)
    {
        //충격파가 플레이어와 충돌한 경우
        if (other.CompareTag("Player"))
        {
            //플레이어에게 충격파 세기 만큼의 데미지 입힘
            PlayerHealth player_health = other.GetComponent<PlayerHealth>();
            player_health.Damage(power);
        }
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}

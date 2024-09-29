using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private SpriteRenderer sr;
    private GameObject Player;
    private Color originColor;
    [SerializeField] bool canAttack = false;
    [SerializeField] private float GroundAttackPower;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Player = GameObject.FindWithTag("Player");
        originColor = sr.color;
    }

    private void Update()
    {
        if (canAttack && (Player.GetComponent<PlayerController>().groundRayHit.distance < 2.5f))
        {
            canAttack = false;
            Debug.Log("땅에 의해 공격 받음");
            Player.GetComponent<PlayerHealth>().Damage(GroundAttackPower);
        }
        
    }

    public void ActiveAttack()
    {
        sr.color = Color.red;
        canAttack = true;
    }
    
    public void DisactiveAttack()
    {
        sr.color = originColor;
        canAttack = false;
    }

}

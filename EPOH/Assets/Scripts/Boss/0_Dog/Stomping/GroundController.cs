using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private SpriteRenderer sr;
    private GameObject player;
    private Color originColor;
    [SerializeField] bool canAttack = false;
    [SerializeField] private float GroundAttackPower;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>().gameObject;
        originColor = sr.color;
    }

    private void Update()
    {
        if (canAttack && (player.GetComponent<PlayerController>().groundRayHit.collider.name == "Ground"))
        {
            canAttack = false;
            Debug.Log($"땅에 의해 공격 받음 {player.GetComponent<PlayerController>().groundRayHit.collider}");
            player.GetComponent<PlayerHealth>().Damage(GroundAttackPower);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompingWave : MonoBehaviour
{
    private GameObject boss;
    private void Start()
    {
        boss = GameObject.FindWithTag("Boss");
    }

    public void GenerateExplosion()
    {
        boss.GetComponent<StompingNew>().GenerateExplosion();
    }

    public void DestroyExplosion()
    {
        StartCoroutine(boss.GetComponent<StompingNew>().DestroyExplosion());
    }
}

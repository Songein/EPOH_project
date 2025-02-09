using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : Attackable
{
    void Start()
    {
        StartCoroutine(StartRotation());
    }

    IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(StopSkill());
    }
}

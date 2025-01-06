using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;

public class Cocoa : Attackable
{
    private void Start()
    {
        StartCoroutine(StopSkill());
    }
}

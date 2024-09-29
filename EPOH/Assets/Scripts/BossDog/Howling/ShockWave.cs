using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : BossSkillBase
{
    public void SetActiveFalse()
    {
        Destroy(transform.parent.gameObject);
        gameObject.SetActive(false);
    }
}

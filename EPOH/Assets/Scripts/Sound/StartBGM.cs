using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour
{
    public int index;   
    void Start()
    {
        SoundManager2.instance.PlayBGM(index);
    }
}

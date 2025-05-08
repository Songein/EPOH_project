using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public string followObjectName;

    void Awake()
    {
        if(!String.IsNullOrWhiteSpace(followObjectName))
            GetComponentInChildren<CinemachineVirtualCamera>(true).Follow = GameObject.Find(followObjectName).transform;
    }
}

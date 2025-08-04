using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inital_Location : MonoBehaviour
{
    public static inital_Location instance;

    private void Awake()
    {
        instance = this;
    }

    // public Vector3 beginning;
    public Vector3 MainRoomTest;
    public Vector3 OfficeRoom1;
    public Vector3 OfficeRoom2;
    public Vector3 OfficeRoom3;
    public Vector3 OfficeRoom4;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailManager : MonoBehaviour
{
    public Transform jailDoor; // 감옥 철창
    public Collider2D jailCollider; // 감옥 내부 감지용 콜라이더
    
    public float lowerSpeed = 1f; // 철창이 내려오는 속도
    public float minHeight = -8.5f; // 철창이 멈출 최저 높이
    public float closeHeight = -7.2f; // 철창이 닫힌 것으로 판단할 높이 기준
    
    private bool isLowering = true;
    public bool jailClosed = false;
    
    private void OnEnable()
    {
        // 활성화되면 천천히 하강
        StartCoroutine(LowerJailDoor());
    }

    private IEnumerator LowerJailDoor()
    {
        while (jailDoor.position.y > minHeight)
        {
            jailDoor.position -= new Vector3(0, lowerSpeed * Time.deltaTime, 0);
            if (jailDoor.position.y <= closeHeight)
            {
                jailClosed = true;
                Debug.Log("감옥이 범죄자 머리 아래로 내려옴.");
            }
            yield return null;
        }

        // 철창이 최저 높이에 도달하면 감옥 닫힘
        Debug.Log("감옥이 완전히 닫혔습니다!");
    }

    public void IsCriminalInJail()
    {
        
    }
}

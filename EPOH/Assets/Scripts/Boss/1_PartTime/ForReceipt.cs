using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForReceipt : MonoBehaviour
{
    private bool active = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            active = true;
        }
    }

    private void Update()
    {
        // space 키를 누르면 즉시 receipt 오브젝트 삭제
        if (active && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("영수증 삭제 시도, 현재 오브젝트: " + gameObject);
            if (gameObject != null)
            {
                Destroy(gameObject);
                Debug.Log("영수증 삭제 완료");
            }
            else
            {
                Debug.Log("receipt_object가 null입니다. 삭제 실패");
            }

            active = false;
        }
           
    }
}

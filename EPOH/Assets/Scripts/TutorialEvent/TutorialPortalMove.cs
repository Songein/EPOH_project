using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPortalMove : MonoBehaviour
{
    private bool is_interactable = false;

    void Update()
    {
        if (is_interactable && Input.GetKeyDown(KeyCode.Space)) // 상호작용 가능하고 상호작용 키를 누르면
        {
            SceneManager.LoadScene("Corridor"); // Corridor 씬으로 이동
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            is_interactable = true; // 상호작용 가능하도록 설정
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 끝났을 때
        {
            is_interactable = false; // 상호작용 불가능하도록 설정
        }
    }

}


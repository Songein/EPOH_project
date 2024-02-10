using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPortalMove : MonoBehaviour
{

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.Space)) // 오브젝트의 이름이 "Player"인 경우
        {
            SceneManager.LoadScene("Corrider");
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOfficeRoom : MonoBehaviour
{
    PlayerController playerController;
    public void officeSceneChange()
    {
        playerController = GetComponent<PlayerController>();

        // 현재 활성화된 씬 가져오기
        Scene current_scene = SceneManager.GetActiveScene();

        if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && GameManager.instance.boss_clear_info[2])
        {
            SceneManager.LoadScene("OfficeRoom2");
        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom2" && GameManager.instance.boss_clear_info[4])
        {
            SceneManager.LoadScene("OfficeRoom3");
        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && GameManager.instance.boss_clear_info[4])
        {
            SceneManager.LoadScene("OfficeRoom4");
        }
        else
        {
            playerController.is_interacting = false;
        }
        
    }
}





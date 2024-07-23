using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOfficeRoom : MonoBehaviour
{
    PlayerController playerController;
    private string interacting_object;

    public void setInteractingObjectName(string object_name)
    {
        interacting_object = object_name;
    }

    public void officeSceneChange()
    {
        playerController = GetComponent<PlayerController>();

        // 현재 활성화된 씬 가져오기
        Scene current_scene = SceneManager.GetActiveScene();
        Debug.Log("Current scene: " + current_scene.name);
    
        if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && (GameManager.instance.boss_clear_info[2] || GameManager.instance.boss_clear_info[3] || GameManager.instance.boss_clear_info[4]) && interacting_object == "StairsToOfficeRoom2")
        {
            SceneManager.LoadScene("OfficeRoom2");

        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom2" && GameManager.instance.boss_clear_info[4] && interacting_object == "ElevatorToOfficeRoom3")
        {
            SceneManager.LoadScene("OfficeRoom3");
        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && GameManager.instance.boss_clear_info[4] && interacting_object == "ElevatorToOfficeRoom4")
        {
            SceneManager.LoadScene("OfficeRoom4");
        }
        else
        {
            PlayerInteract.instance.is_interacting = false;
        }
        
    }
}





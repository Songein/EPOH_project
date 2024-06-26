using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToOfficeRoom : MonoBehaviour
{
    public void officeSceneChange()
    {
        string[] OR = {"OfficeRoom2", "OfficeRoom3"}; 

        if (GameManager.instance != null && GameManager.instance.office_room >= 0 && GameManager.instance.office_room < OR.Length)
        {
            string office_scene_name = OR[GameManager.instance.office_room];

            SceneManager.LoadScene(office_scene_name);
        }
        else
        {
            Debug.LogError("Invalid office_room index or GameManager instance is null.");
        }
        
    }
}





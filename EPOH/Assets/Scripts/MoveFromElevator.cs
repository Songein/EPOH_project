using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFromElevator : MonoBehaviour
{
    PlayerController playerController;

    public void operateElevator()
    {
        playerController = GetComponent<PlayerController>();

        if(GameManager.instance != null && GameManager.instance.boss_clear_info[3])
        {
            SceneManager.LoadScene("OfficeRoom4");
        }
        else
        {
            PlayerInteract.instance.is_interacting = false;
        }
    }
}

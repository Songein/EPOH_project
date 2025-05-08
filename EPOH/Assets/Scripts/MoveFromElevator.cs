using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFromElevator : MonoBehaviour
{

    public void operateElevator()
    {

        if(GameManager.instance != null && GameManager.instance.bossClearInfo[3])
        {
            SceneManager.LoadScene("OfficeRoom4");
        }
        else
        {
            PlayerInteract.instance.is_interacting = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToGameover : MonoBehaviour
{
   
    public void sceneChange()
    {
        SceneManager.LoadScene("Gameover");
    }
}


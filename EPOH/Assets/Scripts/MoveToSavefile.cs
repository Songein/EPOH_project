using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToSavefile : MonoBehaviour
{
   
    public void sceneChange()
    {
        SceneManager.LoadScene("Savefile");
    }
}
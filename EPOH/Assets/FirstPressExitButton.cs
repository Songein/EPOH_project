using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstPressExitButton : MonoBehaviour
{
    public void onStartButtonClick()
    {
        Application.Quit();
    }
}

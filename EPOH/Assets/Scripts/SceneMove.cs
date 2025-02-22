using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public GameObject SoundPanel;
   public void GoToCriminal() {
        SceneManager.LoadScene("Criminal");
    }

    public void Hoa() {
        SceneManager.LoadScene("BossRoomHoaSH");
    }

    public void Main()
    {
        SceneManager.LoadScene("MainRoom");
    }
    public void Activate() {
        SoundPanel.SetActive(true);

    }
}

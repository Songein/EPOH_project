using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForResetBtn : MonoBehaviour
{
    public GameObject setttings;
    //기록없애고 MainRoom으로
    public void Reset_new()
    {

        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        if (File.Exists(path))
        {
            File.Delete(path);
            SaveManager.instance.ResetSaveData();
            Debug.Log("데이터 지움");
        }
        GameManager.instance.if_first = true;
        setttings.SetActive(false);
     
    }

}

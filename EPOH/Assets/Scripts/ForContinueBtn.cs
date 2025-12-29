using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForContinueBtn : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    void Start()
    {
        continueButton.interactable = SaveManager.instance.HasSavedGame();
    }




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

        SceneManager.LoadScene("Beginning");
    }






    //기록없애고 MainRoom으로
    public void Reset()
    {
        
        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        if (File.Exists(path))
        {
            File.Delete(path);
            SaveManager.instance.ResetSaveData();
        }

        SceneManager.LoadScene("MainRoomTest");
    }


    //이어하기 기능
    /*
    public void CheckGameData()
    {
        continueButton.interactable = SaveManager.instance.HasSavedGame();

        if (continueButton.interactable == true) {
            SaveManager.instance.LoadGameState();
        }
    }
    */

}

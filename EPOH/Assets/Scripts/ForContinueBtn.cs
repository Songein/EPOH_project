using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForContinueBtn : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    //기록없애고 MainRoom으로
    public void Reset()
    {
        continueButton.interactable = SaveManager.instance.HasSavedGame();

        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        SceneManager.LoadScene("Mainroom");
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

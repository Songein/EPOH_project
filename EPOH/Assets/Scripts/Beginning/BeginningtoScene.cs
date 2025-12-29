using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningtoScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        string sceneName = SaveManager.instance.SceneName;

        switch (sceneName)
        {
            case "MainRoomTest":
                SceneManager.LoadScene("MainRoomTest");
                break;
            case "OfficeRoom1":
                SceneManager.LoadScene("OfficeRoom1");
                break;
            case "OfficeRoom2":
                SceneManager.LoadScene("OfficeRoom2");
                break;
            case "OfficeRoom3":
                SceneManager.LoadScene("OfficeRoom3");
                break;
            case "OfficeRoom4":
                SceneManager.LoadScene("OfficeRoom4");
                break;
            case "BossRoomDog":
                SceneManager.LoadScene("OfficeRoom1");  
                break;
            case "BossRoomPartTime":
                SceneManager.LoadScene("OfficeRoom2");  
                break;
            case "BossRoomForgetMeNot":
                SceneManager.LoadScene("OfficeRoom3");
                break;
            case "BossRoomHoa":
                SceneManager.LoadScene("OfficeRoom4");
                break;

            default:
                Debug.LogWarning("저장된 씬 이름이 유효하지 않음: " + sceneName);
                SceneManager.LoadScene("MainRoomTest"); // 또는 새 게임 화면
                break;
        }
    }

   
}

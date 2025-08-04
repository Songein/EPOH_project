using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;


public class FirstPressStartButton : MonoBehaviour
{
    public void onStartButtonClick()
    {
        SceneManager.LoadScene("Beginning");
        //string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        //if (GameManager.instance.if_first) // 게임 실행 후 '시작 버튼' 첫 클릭시
        //{
        //    GameManager.instance.if_first = false; // GameManager에 시작버튼 첫 클릭 기록
        //    SceneManager.LoadScene("Beginning"); // Beginning 씬으로 이동
        //}
        //else
        //{
        //    SceneManager.LoadScene("MainRoomTest"); // 게임 실행 후 '시작 버튼' 첫 클릭이 아닐 경우 MainRoom으로 이동

        //}

        //if (!File.Exists(path))
        //{ //저장 파일 없으면 다시 beginning으로 Reset 실행
        //    SceneManager.LoadScene("Beginning");
        //    return;
        //}
        //switch (SaveManager.instance.SceneName)
        //{

        //    case "MainRoomTest":
        //        SceneManager.LoadScene("MainRoomTest");
        //        break;
        //    case "OfficeRoom1":
        //        SceneManager.LoadScene("OfficeRoom1");
        //        break;
        //    case "OfficeRoom2":
        //        SceneManager.LoadScene("OfficeRoom2");
        //        break;
        //    case "OfficeRoom3":
        //        SceneManager.LoadScene("OfficeRoom3");
        //        break;
        //    case "OfficeRoom4":
        //        SceneManager.LoadScene("OfficeRoom4");
        //        break;
        //    case "BossRoomDog":
        //        SceneManager.LoadScene("OfficeRoom1");
        //        break;
        //    case "BossRoomPartTime":
        //        SceneManager.LoadScene("OffiecRoom2");
        //        break;
        //    case "BossRoomForgetMeNot":
        //        SceneManager.LoadScene("OfficeRoom3");
        //        break;
        //    case "BossRoomHoa":
        //        SceneManager.LoadScene("OfficeRoom4");
        //        break;

        //    default:
        //        Debug.LogWarning("알 수 없는 씬 이름: " + SaveManager.instance.SceneName);
        //        SceneManager.LoadScene("Beginning");
        //        break;
        //}


    }




}

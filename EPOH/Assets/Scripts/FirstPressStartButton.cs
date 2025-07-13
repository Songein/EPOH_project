using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FirstPressStartButton : MonoBehaviour
{
    public void onStartButtonClick()
    {
        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
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
        if (GameManager.instance.if_first) // 게임 실행 후 '시작 버튼' 첫 클릭시
        {
            GameManager.instance.if_first = false; // GameManager에 시작버튼 첫 클릭 기록
            SceneManager.LoadScene("Beginning"); // Beginning 씬으로 이동

        }
        else if (SaveManager.instance.progressId == "Progress_Req1_Start" || SaveManager.instance.progressId == "Progress_Req1_Fail" || SaveManager.instance.progressId == "Progress_Req1_Clear")
        {
            SceneManager.LoadScene("OfficeRoom1");
        }
        else
        {
            SceneManager.LoadScene("MainRoomTest");
        }


    }




}

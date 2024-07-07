using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPressStartButton : MonoBehaviour
{
    public void onStartButtonClick()
    {
        if (GameManager.instance.if_first) // 게임 실행 후 '시작 버튼' 첫 클릭시
        {
            GameManager.instance.if_first = false; // GameManager에 시작버튼 첫 클릭 기록
            SceneManager.LoadScene("Beginning"); // Beginning 씬으로 이동
        }
        else
        {
            SceneManager.LoadScene("MainRoom"); // 게임 실행 후 '시작 버튼' 첫 클릭이 아닐 경우 MainRoom으로 이동
        }
    }
}

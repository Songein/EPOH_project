using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToNextScene : MonoBehaviour
{
    public string next_scene_name = "NextScene"; // 다음 씬의 이름을 설정할 변수

    public void sceneChange()
    {
        SceneManager.LoadScene(next_scene_name); // nextSceneName 변수를 사용하여 씬을 로드
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public string progressId = "num_1";  // progressId를 SaveManager 클래스에서 직접 관리
   

    [System.Serializable]
    public class GameState
    {
        
        public string progressId;
       
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);

        }
    }


    // SaveGameState 수정: progressId도 함께 저장
    //progressId를 바꿔준 다음 SaveGameState을 불러와줘야함
    public void SaveGameState()
    {
        GameState state = new GameState
        {

           progressId = this.progressId  // SaveManager의 progressId 사용
       // progressId =  GameManager.instance.ProgressState.ToString()
        };

        string json = JsonUtility.ToJson(state,true);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/gameState.json", json);
    }

    // LoadGameState 수정: progressId도 불러오기
    public void LoadGameState()
    {
        string path = Application.persistentDataPath + "/gameState.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameState state = JsonUtility.FromJson<GameState>(json);
            // 불러온 상태 적용
           
            progressId = state.progressId;  // progressId 업데이트
            Debug.Log("Progress ID: " + progressId);
            GoToScene();
        }
    }

    public bool HasSavedGame()
    {
        return File.Exists(Application.persistentDataPath + "/gamedata.json");
    }


    // GettheId 수정: progressId를 업데이트하고 반환
    public string GettheId(string id)
    {
       this.progressId = id;  // SaveManager 클래스 내의 progressId를 업데이트
        return this.progressId; // 반환
    }


    /*
    ///scene 관리 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneId = scene.name;
        Debug.LogWarning("Scene loaded: " + sceneId);
        SaveGameState();

    }
    */
    
    private void GoToScene() {
        SceneManager.LoadScene("MainRoom");
    }


    /*
    private void GoToScene() {
        if (this.progressId == "Player_Clear")
        {
            SceneManager.LoadScene("MainRoom");
        }
        else if (this.progressId == "Progress_Hoa_Start") {
            SceneManager.LoadScene("BossRoomHoaSH");
        }
    }

    private void IdManage(string id)
    {
        if (id == "MainRoom") // 씬이 Main이면 초기화
        {
            this.progressId = "Player_Clear";
            SaveGameState();

        }
        else if (id == "BossRoomHoaSH")
        {
            this.progressId = "Progress_Hoa_Start";
            SaveGameState();
        }
        else {
            return;
        }
    }
    */

}
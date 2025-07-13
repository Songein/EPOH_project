using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public Transform player;


    public static SaveManager instance;



    public Vector3 player_Pos;
    public string progressId;  // progressId를 SaveManager 클래스에서 직접 관리
    public string eventId;
    public bool[] bossClearInfo;
    public float x;
    public float y;
    public float z;



    //종료 될때 위치 저장
    void OnApplicationQuit()
    {
        SavePlayerPosition();
        //SaveGameState();
    }


    void SavePlayerPosition()
    {


            GameObject foundPlayer = GameObject.FindWithTag("Player");
             player = foundPlayer.transform;
                Debug.Log("Player 찾음: " + player.position);
            

            Vector3 player_Pos = player.position;
                this.player_Pos = player_Pos;
            Debug.Log("플레이어 위치 저장됨: "+ player_Pos);
        
    }

    [System.Serializable]
    public class GameState
    {
        
        public string progressId;
        public string eventId;
        public bool[] bossClearInfo;

        public float x;
        public float y;
        public float z;

    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameState();
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

            progressId = this.progressId, // SaveManager의 progressId 사용
            eventId = this.eventId,
            bossClearInfo = GameManager.instance.bossClearInfo,

            x = this.player_Pos.x,
            y = this.player_Pos.y,
            z = this.player_Pos.z

            // progressId =  GameManager.instance.ProgressState.ToString()
        };

        string json = JsonUtility.ToJson(state,true);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);
    }

    // LoadGameState 수정: progressId도 불러오기
    public void LoadGameState()
    {
        string path = Application.persistentDataPath + "/gamedata.json";
        if (File.Exists(path))
        {
            GameManager.instance.if_first = false; // GameManager에 시작버튼 첫 클릭 기록
            string json = File.ReadAllText(path);
            GameState state = JsonUtility.FromJson<GameState>(json);
            // 불러온 상태 적용
           
            progressId = state.progressId;  // progressId 업데이트
            eventId = state.eventId;
            bossClearInfo = state.bossClearInfo;
            x = state.x;
            y = state.y;
            z = state.z;

            //GameManager에게도 progress 넘기기
            GameManager.instance.SetProgressByString(progressId);

            //이 부분은 GameManager에서 SaveManager값을 가져오는 것으로 대체
           // GameManager.instance.LoadBossClearInfo(bossClearInfo);
            Debug.Log("Progress ID 로드 및 적용 완료: " + progressId);
        }
    }

    public bool HasSavedGame()
    {
        return File.Exists(Application.persistentDataPath + "/gamedata.json");
    }


    // GettheId 수정: progressId를 업데이트하고 반환
    public void GettheId(string id)
    {
       this.progressId = id;  // SaveManager 클래스 내의 progressId를 업데이트
        Debug.Log(id + " 저장!뽀꾸뽀꾸");
        SaveGameState();
  
    }


   //초기화 함수, Reset에서 불러올 것임
    public void ResetSaveData()
    {
        // progressId, eventId 초기화 (초기값에 맞게 수정하세요)
        progressId = ""; // 예시 초기 상태
        eventId = "";                      // 초기 상태가 빈 문자열이라면 이렇게

        // GameManager의 bossClearInfo 배열 초기화
        for (int i = 0; i < 5; i++)
        {
            GameManager.instance.bossClearInfo[i] = false;
            SaveManager.instance.bossClearInfo[i] = false;
        }

        // 플레이어 위치 초기화 (원하는 초기 위치 값으로 세팅)
        player_Pos = new Vector3(-9.5f, -8.16f, -1f);

        Debug.Log("SaveManager 데이터 초기화 완료");
    }

    public void GettheEvent(string eventId)
    {
        this.eventId = eventId;  // SaveManager 클래스 내의 progressId를 업데이트
        Debug.Log(eventId + " 저장!이벤트뽀꾸뽀꾸");
        SaveGameState();

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool if_first; // Start 씬의 '시작 버튼'최초 클릭 여부 기록
    public bool items_all_collected; // 동료들의 소지품 모두 회수했는지에 대한 정보
    public int story_info;
    public int office_room;
    public int boss_num = 0;
    public const int boss_cnt = 5; // 존재하는 보스의 개수
    
    //씬 전반적인 이벤트 관리 변수
    public bool eventFlag;

    // 보스 정보. 이름, 속성으로 이루어짐.
    public string[,] boss_info = new string[,] { { "Dog" , "1" }, { "PartTime", "2" }, { "ForgetMeNot", "3" }, {"Criminal", "4"}, {"Hoa", "5"} }; 

    public bool[] boss_clear_info = new bool[boss_cnt]; // 보스 클리어 여부 확인

    public GameState gameState;
    public BossRoomState bossRoomState;
    public EndingState endingState;

    public enum GameState
    {
        GameStart,
        Tutorial,
        Dog,
        PartTime,
        ForgetNotMe,
        Criminal,
        GetOffWork,
        Ending,
        GameEnd
    }

    public enum BossRoomState
    {
        BeforeMissionAssignment,
        MissionAssignment,
        EnterBossRoom,
        MissonComplete,
    }

    public enum EndingState
    {
        NormalEnding,
        BadEnding,
        HiddenEnding
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if_first = true;

        items_all_collected = false;

        boss_clear_info[0] = true;
        boss_clear_info[1] = false;
        boss_clear_info[2] = false;
        boss_clear_info[3] = false;
        boss_clear_info[4] = false;
        boss_clear_info[5] = false;
    }
}

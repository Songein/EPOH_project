using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    public static GameManager_ Instance;
    public int storyNum;
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
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool if_first; // Start 씬의 '시작 버튼'최초 클릭 여부 기록
    public const int bossCnt = 5; // 존재하는 보스의 개수
    
    // 보스 클리어 상태
    public bool[] bossClearInfo = new bool[bossCnt]; // 보스 클리어 여부 확인
    // 보스 아이템 획득 상태
    public bool[] bossObjectAcquiredInfo = new bool[bossCnt - 1];

    public ProgressId ProgressState;

    public enum ProgressId
    {
        Progress_Beginning,
        Progress_Req1,
        Progress_Req1_Start,
        Progress_Req1_Fail,
        Progress_Req1_Clear,
        Progress_Req2,
        Progress_Req2_Start,
        Progress_Req2_Fail,
        Progress_Req2_Clear,
        Progress_Req3,
        Progress_Req3_Start,
        Progress_Req3_Fail,
        Progress_Req3_Clear,
        Progress_Req4,
        Progress_Req4_Start,
        Progress_Req4_Fail,
        Progress_Req4_Clear,
        Progress_EndPoint,
        Progress_Ending1,
        Progress_Ending2,
        Progress_Hoa_Start,
        Progress_Hoa_Fail,
        Progress_Ending3
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

        for (int boss = 0; boss < bossClearInfo.Length; boss++)
        {
            bossClearInfo[boss] = false;
        }

        for (int item = 0; item < bossObjectAcquiredInfo.Length; item++)
        {
            bossObjectAcquiredInfo[item] = false;
        }
    }
}

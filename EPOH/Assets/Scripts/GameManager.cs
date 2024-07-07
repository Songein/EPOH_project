using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤으로 사용할 수 있도록 static 선언

    public bool if_first; // Start 씬의 '시작 버튼'최초 클릭 여부 기록

    public bool items_all_collected; // 동료들의 소지품 모두 회수했는지에 대한 정보

    public int office_room;

    public int story_info = 0; // 스토리 진행 정도에 대한 정보
    //public bool[] rune = new bool[5]; // 룬이 활성화 되었는가에 대한 정보. 순서대로 Anger, Fear, Humiliation, Sorrow, Regret

    public int boss_num = 0; //위젯에서 선택한 보스의 인덱스
    public const int boss_cnt = 5; // 존재하는 보스의 개수
    
    // 보스 정보. 이름, 속성으로 이루어짐.
    public string[,] boss_info = new string[,] { { "Dog" , "1" }, { "PartTime", "2" }, { "ForgetMeNot", "3" }, {"Criminal", "4"}, {"Hoa", "5"} }; 

    public bool[] boss_clear_info = new bool[boss_cnt]; // 보스 클리어 여부 확인

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

    }
}

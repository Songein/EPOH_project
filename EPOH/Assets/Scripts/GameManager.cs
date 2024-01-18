using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤으로 사용할 수 있도록 static 선언

    public int story_info = 0; // 스토리 진행 정도에 대한 정보
    public bool[] rune = new bool[5]; // 룬이 활성화 되었는가에 대한 정보. 순서대로 Anger, Fear, Humiliation, Sorrow, Regret

    public int boss_num = 0; //위젯에서 선택한 보스의 인덱스
    public const int boss_cnt = 3; // 존재하는 보스의 개수
    
    // 보스 정보. 이름, 속성으로 이루어짐.
    public string[,] boss_info = new string[,] { { "Dog" , "1" }, { "ForgetMeNot", "3" }, { "PartTime", "2" } }; 

    public bool[] boss_clear_info = new bool[boss_cnt]; // 보스 클리어 여부 확인

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

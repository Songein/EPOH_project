using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤으로 사용할 수 있도록 static 선언

    public bool if_first = true; // Start 씬의 '시작 버튼'최초 클릭 여부 기록

    public bool if_revive = false; // 보스전에서 플레이어가 Revive 했는지 여부

    public int story_info = 0; // 스토리 진행 정도에 대한 정보
    public int tutorial_info = 0;

    public int boss_num = 0; // 진행중인 보스의 인덱스
    public const int boss_cnt = 5; // 존재하는 보스의 개수
    
    // 보스 정보. 이름으로 이루어짐.
    public string[] boss_info = new string[] { "Dog", "PartTime", "ForgetMeNot", "Criminal", "Hoa"}; 
    
    public bool is_back = false; //클리어 후 돌아오는 길인지 corrider에서 확인하기 위한 변수

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
    }
}

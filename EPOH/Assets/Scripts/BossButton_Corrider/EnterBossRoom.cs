using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossRoom : MonoBehaviour
{
    //private GameManager game_manager; // GameManager 스크립트에 대한 참조 변수
    /*public Button move_to_right_button; // MoveToRight device 버튼

    void Start()
    {
        if (move_to_right_button != null)
        {
            move_to_right_button.onClick.AddListener(() => moveToBossRoom());
        }
    }
    */


    public void moveToBossRoom()
    {
        if (GameManager.instance != null)
        {
            int boss_index = GameManager.instance.boss_num;
            string boss_room_scene_name = "";

            if (boss_index >= 0 && boss_index < GameManager.boss_cnt)
            {
                boss_room_scene_name = "BossRoom" + GameManager.instance.boss_info[boss_index, 0]; // 보스룸 씬 이름
                Debug.Log("Boss Room Scene Name: " + boss_room_scene_name);
            }
            else
            {
                Debug.LogWarning("해당하는 보스룸 씬을 찾을 수 없습니다.");
                return;
            }

            // 보스룸으로 이동
            SceneManager.LoadScene(boss_room_scene_name);
        }
        
    }
}
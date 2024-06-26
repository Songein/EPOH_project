using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossRoom : MonoBehaviour
{
    PlayerController playerController;

    public void moveToBossRoom()
    {
        playerController = GetComponent<PlayerController>();


        // 현재 활성화된 씬 가져오기
        Scene current_scene = SceneManager.GetActiveScene();
        
        if (GameManager.instance != null && current_scene.name == "OfficeRoom1")
        {
            if (GameManager.instance.boss_clear_info[0] && this.gameObject.name == "BossDog")
            {
                Debug.Log("BossDog와 상호작용했습니다.");
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomDog");
                Debug.Log("BossRoomDog Scene으로 이동");
            }
            else if (GameManager.instance.boss_clear_info[1] && this.gameObject.name == "BossPartTime")
            {
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomPartTime");
                Debug.Log("BossRoomPartTime Scene으로 이동");
            }
            else
            {
                Debug.LogWarning("조건에 맞지 않거나 GameManager instance를 찾을 수 없습니다.");
            }
        }
        else{
            playerController.is_interacting = false;
            Debug.LogWarning("GameManager instance not found.");
        }
        
    }
}
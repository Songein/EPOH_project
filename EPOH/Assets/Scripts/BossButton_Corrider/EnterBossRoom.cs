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
                Debug.Log("BossPartTime과 상호작용했습니다.");
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomPartTime");
                Debug.Log("BossRoomPartTime Scene으로 이동");
            }
            else
            {
                Debug.LogWarning("조건에 맞지 않거나 GameManager instance를 찾을 수 없습니다.");
            }
        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom2")
        {
            if (GameManager.instance.boss_clear_info[2] && this.gameObject.name == "BossForgetMeNot")
            {
                Debug.Log("BossForgetMeNot과 상호작용했습니다.");
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomForgetMeNot");
                Debug.Log("BossRoomForgetMeNot Scene으로 이동");
            }
            else if (GameManager.instance.boss_clear_info[3] && this.gameObject.name == "BossCriminal")
            {
                Debug.Log("BossCriminal과 상호작용했습니다.");
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomCriminal");
                Debug.Log("BossRoomCriminal Scene으로 이동");
            }
            else
            {
                Debug.LogWarning("조건에 맞지 않거나 GameManager instance를 찾을 수 없습니다.");
            }
        }
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom3")
        {
            if (GameManager.instance.boss_clear_info[4] && this.gameObject.name == "BossHoa")
            {
                Debug.Log("BossHoa와 상호작용했습니다.");
                // 보스룸으로 이동
                SceneManager.LoadScene("BossRoomHoa");
                Debug.Log("BossRoomHoa Scene으로 이동");
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
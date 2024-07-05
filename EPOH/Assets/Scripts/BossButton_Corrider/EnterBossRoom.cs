using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossRoom : MonoBehaviour
{
    PlayerController playerController;
    private string interacting_object;

    public void setInteractingObjectName(string object_name)
    {
        interacting_object = object_name;
    }

    public void moveToBossRoom()
    {
        playerController = GetComponent<PlayerController>();


        // 현재 활성화된 씬 가져오기
        Scene current_scene = SceneManager.GetActiveScene();
        Debug.Log("Current scene: " + current_scene.name);


        if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && GameManager.instance.boss_clear_info[0] && interacting_object == "BossDog")
        {
            Debug.Log("BossDog와 상호작용했습니다.");
            // 보스룸으로 이동
            SceneManager.LoadScene("BossRoomDog");
            Debug.Log("BossRoomDog Scene으로 이동");
        }
            
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom1" && GameManager.instance.boss_clear_info[1] && interacting_object == "BossPartTime")
        {
            Debug.Log("BossPartTime과 상호작용했습니다.");
            // 보스룸으로 이동
            SceneManager.LoadScene("BossRoomPartTime");
            Debug.Log("BossRoomPartTime Scene으로 이동");
        }
        
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom2" && GameManager.instance.boss_clear_info[2] && interacting_object == "BossForgetMeNot")
        {
            Debug.Log("BossForgetMeNot과 상호작용했습니다.");
            // 보스룸으로 이동
            SceneManager.LoadScene("BossRoomForgetMeNot");
            Debug.Log("BossRoomForgetMeNot Scene으로 이동");
        }
        
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom2" && GameManager.instance.boss_clear_info[3] && interacting_object == "BossCriminal")
        {
            Debug.Log("BossCriminal과 상호작용했습니다.");
            // 보스룸으로 이동
            SceneManager.LoadScene("BossRoomCriminal");
            Debug.Log("BossRoomCriminal Scene으로 이동");
        }
    
        else if (GameManager.instance != null && current_scene.name == "OfficeRoom3" && GameManager.instance.boss_clear_info[4] && interacting_object == "BossHoa")
        {       
            Debug.Log("BossHoa와 상호작용했습니다.");
            // 보스룸으로 이동
            SceneManager.LoadScene("BossRoomHoa");
            Debug.Log("BossRoomHoa Scene으로 이동");
        }

        else
        {
            playerController.is_interacting = false;
            Debug.LogWarning("조건에 맞지 않거나 GameManager instance를 찾을 수 없습니다.");
        }
        
    }
}
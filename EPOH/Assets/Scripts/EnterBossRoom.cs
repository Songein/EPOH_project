using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossRoom : MonoBehaviour
{
    public Button moveToRightButton; // MoveToRight device 버튼
    private GameManager gameManager; // GameManager 스크립트에 대한 참조 변수

    void Start()
    {
        if (moveToRightButton != null)
        {
            moveToRightButton.onClick.AddListener(() => MoveToBossRoom());
        }

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다.");
            return;
        }

        
    }

    void MoveToBossRoom()
    {
        int bossIndex = gameManager.boss_num;
        string bossRoomSceneName = "";

        // 보스 인덱스에 따라 보스 룸 씬의 이름 설정
        switch (bossIndex)
        {
            case 0:
                bossRoomSceneName = "BossRoom_Dog";
                break;
            case 1:
                bossRoomSceneName = "BossRoom_ForgetMeNot";
                break;
            case 2:
                bossRoomSceneName = "BossRoom_PartTime";
                break;
            default:
                Debug.LogWarning("해당하는 보스 룸 씬을 찾을 수 없습니다.");
                break;
        }

        // 보스 룸 씬으로 전환
        SceneManager.LoadScene(bossRoomSceneName);
    }

    
}

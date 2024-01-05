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

        if (bossIndex >= 0 && bossIndex < GameManager.boss_cnt)
        {
            bossRoomSceneName = "BossRoom" + gameManager.boss_info[bossIndex, 0]; // 보스 룸 씬의 이름 설정
        }
        else
        {
            Debug.LogWarning("해당하는 보스 룸 씬을 찾을 수 없습니다.");
            return;
        }

        // 보스 룸 씬으로 전환
        SceneManager.LoadScene(bossRoomSceneName);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossRoom : MonoBehaviour
{
    public Button move_to_right_button; // MoveToRight device ��ư
    // private GameManager game_manager; // GameManager ��ũ��Ʈ�� ���� ���� ����

    void Start()
    {
        if (move_to_right_button != null)
        {
            move_to_right_button.onClick.AddListener(() => moveToBossRoom());
        }
    }

    void moveToBossRoom()
    {
        int boss_index = GameManager.instance.boss_num;
        string boss_room_scene_name = "";

        if (boss_index >= 0 && boss_index < GameManager.boss_cnt)
        {
            boss_room_scene_name = "BossRoom" + GameManager.instance.boss_info[boss_index, 0]; // ���� �� ���� �̸� ����
        }
        else
        {
            Debug.LogWarning("�ش��ϴ� ���� �� ���� ã�� �� �����ϴ�.");
            return;
        }

        // ���� �� ������ ��ȯ
        SceneManager.LoadScene(boss_room_scene_name);
    }
}

using UnityEngine;

public class ActivateRune : MonoBehaviour
{
    public GameObject rune_anger; // Rune Anger 오브젝트
    public GameObject rune_fear; // Rune Fear 오브젝트
    public GameObject rune_humiliation; // Rune Humiliation 오브젝트
    public GameObject rune_sorrow; // Rune Sorrow 오브젝트
    public GameObject rune_regret; // Rune Regret 오브젝트


    private Color[] colors = new Color[5]
    {
        Color.red,       // 빨간색
        new Color(1.0f, 0.5f, 0.0f), // 주황색
        Color.green,     // 초록색
        Color.blue,      // 파란색
        new Color(0.5f, 0.0f, 0.5f) // 보라색
    };

    void Start()
    {
        // 초기 색상 설정 : 모두 하얀색으로 초기화
        applyColorToGameObject(rune_anger, Color.white); 
        applyColorToGameObject(rune_fear, Color.white);
        applyColorToGameObject(rune_humiliation, Color.white);
        applyColorToGameObject(rune_sorrow, Color.white); 
        applyColorToGameObject(rune_regret, Color.white);


        // GameManager 스크립트에 대한 참조 가져오기
        GameManager game_manager = FindObjectOfType<GameManager>();

        if (game_manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다.");
            return;
        }
        /*
        // 보스룸 Dog가 클리어되면 Rune Anger를 활성화
        if (GameManager.instance.boss_clear_info[0])
        {
            GameManager.instance.rune[0] = true; 
            activateRuneColor(rune_anger, true, 0); // Rune Anger를 활성화
        }*/

        //보스룸 oo가 클리어되면 Rune Fear를 활성화

        //보스룸 oo가 클리어되면 Rune Humiliation를 활성화

        //보스룸 oo가 클리어되면 Rune Sorrow를 활성화

        //보스룸 oo가 클리어되면 Rune Regret를 활성화


    }

    void activateRuneColor(GameObject rune_object, bool isActive, int colorIndex)
    {
        if (isActive)
        {
            applyColorToGameObject(rune_object, colors[colorIndex]);
        }
        else
        {
            Debug.LogWarning("Rune이 활성화되지 않았습니다.");
        }
    }

    void applyColorToGameObject(GameObject obj, Color color)
    {
        // GameObject에 색상 적용
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer가 없거나 비활성화되어 있습니다.");
        }
    }
}

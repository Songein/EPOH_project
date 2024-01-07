using UnityEngine;

public class ApplyColorOnRune : MonoBehaviour
{
    public GameObject rune_object; // 룬에 해당하는 GameObject

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
        // 초기 색상 설정
        applyColorToGameObject(rune_object, Color.white); // 흰색으로 초기화

        // GameManager 스크립트에 대한 참조 가져오기
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다.");
            return;
        }

        setRuneColor(gameManager.rune);
    }

    void setRuneColor(bool[] runes)
    {
        int activeIndex = -1;
        for (int i = 0; i < runes.Length; i++)
        {
            if (runes[i])
            {
                if (activeIndex != -1) // 이미 다른 값이 true인 경우
                {
                    Debug.LogError("오직 하나의 값만 true여야 합니다.");
                    return;
                }
                activeIndex = i;
            }
        }

        if (activeIndex != -1) // 하나의 값만 true인 경우에만 색상을 적용
        {
            applyColorToGameObject(rune_object, colors[activeIndex]);
        }
        else
        {
            Debug.LogWarning("하나의 값이 true여야 합니다.");
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

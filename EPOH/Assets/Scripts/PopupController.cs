using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popup; // 팝업창을 나타내는 GameObject
    public Button return_button; // 돌아가기 버튼을 나타내는 Button 컴포넌트
    private bool is_open = false; // 팝업이 열려있는지 여부
    private PlayerController playerController; // PlayerController 참조 변수 추가

    void Start()
    {
        // 팝업창을 시작할 때는 닫혀있도록 설정
        popup.SetActive(false);

        // PlayerController 참조 가져오기
        playerController = FindObjectOfType<PlayerController>();

        // 확인 버튼에 클릭 이벤트 연결
        if (return_button != null)
        {
            return_button.onClick.AddListener(closePopupAndSetInteractingFalse);
        }
    }

    // 상호작용시 호출되는 함수
    public void togglePopup()
    {
        // 팝업이 열려있으면 닫고, 닫혀있으면 열도록 설정
        is_open = !is_open;
        popup.SetActive(is_open);
    }

    public void closePopupAndSetInteractingFalse()
    {
        popup.SetActive(false);

        // PlayerController가 존재하고 is_interacting를 false로 설정
        if (playerController != null)
        {
            playerController.is_interacting = false;
        }
        
    }
}

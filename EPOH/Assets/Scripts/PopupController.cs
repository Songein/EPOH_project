using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popup; // 팝업창을 나타내는 GameObject
    public Button return_button; // 돌아가기 버튼을 나타내는 Button 컴포넌트
    private bool is_open = false; // 팝업이 열려있는지 여부

    void Start()
    {
        // 팝업창을 시작할 때는 닫혀있도록 설정
        popup.SetActive(false);

        // 확인 버튼에 클릭 이벤트 연결
        if (return_button != null)
        {
            return_button.onClick.AddListener(closePopup);
        }
    }

    // 버튼 클릭 시 호출되는 함수
    public void togglePopup()
    {
        // 팝업이 열려있으면 닫고, 닫혀있으면 열도록 설정
        is_open = !is_open;
        popup.SetActive(is_open);
    }


     //팝업 닫기 함수
    public void closePopup()
    {
        popup.SetActive(false);
    }

}

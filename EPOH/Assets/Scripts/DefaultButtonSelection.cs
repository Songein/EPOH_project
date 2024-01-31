using UnityEngine;
using UnityEngine.UI;

public class DefaultButtonSelection : MonoBehaviour
{
    public Button default_button; // Unity 에디터에서 디폴트로 선택하고자 하는 버튼을 Drag & Drop으로 연결할 수 있도록 변수 선언

    public void DefaultSetting()
    {
        // 시작 시 특정 버튼을 선택하도록 설정
        default_button.Select();
    }
}
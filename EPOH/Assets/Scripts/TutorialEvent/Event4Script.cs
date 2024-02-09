using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Event4Script : MonoBehaviour
{
    public ScrollRect scrollRect;

    public GameObject event_panel; //  이벤트 Panel을 저장할 변수
    public TMP_Text event_text; // 이벤트 TMPro Text 요소를 저장할 변수
    private float event_delay = 2f;

    public Button accept_button;

    void Start()
    {
        event_panel.SetActive(false);
        accept_button.onClick.AddListener(closeScrollViewAndOpenEventPanel);
    }

    public void closeScrollViewAndOpenEventPanel()
    {
        scrollRect.gameObject.SetActive(false); // ScrollRect 비활성화
        StartCoroutine(startEvent4Talk());
    }

    IEnumerator startEvent4Talk()
    {
        event_panel.SetActive(true); // 이벤트 Panel 활성화

        //첫 번째 이벤트 안내음
        event_text.text = "안내음: 의뢰가 수리되었습니다. 의뢰인의 기억과 연결합니다.";
        yield return new WaitForSeconds(event_delay);

        // 안내음 사라짐
        event_text.text = "";

        // 두 번째 안내음 표시
        yield return new WaitForSeconds(0.5f); // 안내음 간의 짧은 딜레이
        event_text.text = "안내음: ……";
        yield return new WaitForSeconds(event_delay);

        // 안내음 사라짐
        event_text.text = "";

        // 세 번째 안내음 표시
        yield return new WaitForSeconds(0.5f); // 안내음 간의 짧은 딜레이
        event_text.text = "안내음: 연결 완료. 전사 장치를 통해 기억에 입장해주시기 바랍니다.";
        yield return new WaitForSeconds(event_delay);

        // 안내음 사라짐
        event_text.text = "";

        // 이벤트 Panel 비활성화
        event_panel.SetActive(false);

    }
}

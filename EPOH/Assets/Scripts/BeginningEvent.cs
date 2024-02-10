using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningEvent : MonoBehaviour
{
    public TMP_Text b_text;

    private string[] messages = 
    {
        "대상 식별중…",
        "E801F83499F1D00AE6B6451C445B013880A9906EF783C86EFB671E1CE74C3842CCE97B597E16546AABDE44AB50E816027E1E08F44E297BF89E41C61B1807F6C2",
        "상태: Trance",
        "의식 연결을 시작합니다.",
        "공유 에어리어 접속",
        "아바타 구현화",
        "컴포넌트 H 부착",
        "......",
        "프로세스 완료"
    };

    void Start()
    {
        StartCoroutine(displayMessages());
    }

    IEnumerator displayMessages()
    {
        for (int j = 0; j < messages.Length; j++)
        {
            string message = messages[j];
            for (int i = 0; i < message.Length; i++)
            {
                b_text.text += message[i]; // Add one character at a time
                yield return new WaitForSeconds(0.04f); // adjust speed here
            }
            if (j < messages.Length - 1) // Don't add newline at the end
            {
                b_text.text += "\n"; // Start a new line for each message
            }
            if (message == "......" || message == "프로세스 완료")
            {
                yield return new WaitForSeconds(1f); 
            }

        }

        SceneManager.LoadScene("Tutorial MainRoom");
    }
}

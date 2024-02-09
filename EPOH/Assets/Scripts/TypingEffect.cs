using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    string target_message; // 출력할 메세지
    public int char_per_sec; // 1초에 몇 개의 글자를 출력하는지
    private TextMeshProUGUI message_text; //출력할 텍스트
    public TextMeshProUGUI talk_text; //대화창 텍스트
    public TextMeshProUGUI notice_text; //안내음 텍스트
    int index;

    public void SetMessage(string message, bool notice)
    {
        target_message = message; // 출력할 메세지를 저장
        EffectStart(notice);
    }

    void EffectStart(bool notice)
    {
        if (notice)
        {
            message_text = notice_text;
        }
        else
        {
            message_text = talk_text;
        }
        message_text.text = ""; // 대화창의 text를 비운다
        index = 0; // 인덱스 초기화

        Invoke("Effecting", 1.0f /char_per_sec); // Invoke를 사용해 시간을 지연시켜서 출력
    }
    void Effecting()
    {
        if(message_text.text == target_message) // 문장을 전부 출력했을 경우
        {
            return; // 함수 종료
        }
        message_text.text += target_message[index];
        index++;
        
        Invoke("Effecting", 1.0f / char_per_sec); // 재귀적으로 호출
    }
}

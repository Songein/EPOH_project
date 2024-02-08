using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginningEvent : MonoBehaviour
{
    private string text;
    public TMP_Text target_text;
    private float delay = 0.020f;

    private bool event_occurred = false; // 이벤트 발생 여부를 나타내는 변수

    void Start()
    {
        text = target_text.text.ToString();
        target_text.text = " ";

        if (!event_occurred) // 이벤트가 발생하지 않았을 때만 실행
        {
            StartCoroutine(textPrint(delay));
        }
    }

      IEnumerator textPrint(float d)
    {
        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                target_text.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(delay);
        }

        // 이벤트 출력이 끝나면 MainRoom으로 전환
        SceneManager.LoadScene("Tutorial MainRoom");
        
        event_occurred = true; // 이벤트가 발생했음을 표시
        gameObject.SetActive(false); // 스크립트를 비활성화하여 더 이상 이벤트가 발생하지 않도록 함
    }
}

    


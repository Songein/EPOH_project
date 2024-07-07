using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMessages : MonoBehaviour
{
    public TMP_Text b_text;

    private string[] bad_ending_messages = 
    {
        "Bad Ending...",
        "Try Again..."
    };

    private string[] normal_ending_messages = 
    {
        "Normal Ending...",
        "동료들의 소지품을 모두 회수하는데 성공하셨습니다.",
    };


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.items_all_collected)
        {
            StartCoroutine(displayNormalEndingMessages());
        }
        else
        {
            StartCoroutine(displayBadEndingMessages());
        }
    }

    IEnumerator displayNormalEndingMessages()
    {
        for (int j = 0; j < normal_ending_messages.Length; j++)
        {
            string message = normal_ending_messages[j];
            for (int i = 0; i < message.Length; i++)
            {
                b_text.text += message[i]; // Add one character at a time
                yield return new WaitForSeconds(0.1f); // adjust speed here
            }
            if (j < normal_ending_messages.Length - 1) // Don't add newline at the end
            {
                b_text.text += "\n"; // Start a new line for each message
            }

        }

    }

    IEnumerator displayBadEndingMessages()
    {
        for (int j = 0; j < bad_ending_messages.Length; j++)
        {
            string message = bad_ending_messages[j];
            for (int i = 0; i < message.Length; i++)
            {
                b_text.text += message[i]; // Add one character at a time
                yield return new WaitForSeconds(0.1f); // adjust speed here
            }
            if (j < bad_ending_messages.Length - 1) // Don't add newline at the end
            {
                b_text.text += "\n"; // Start a new line for each message
            }

        }

    }
}

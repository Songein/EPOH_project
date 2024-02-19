using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBeContinue : MonoBehaviour
{
    public TMP_Text b_text;

    private string[] messages = 
    {
        "To Be Continue..."
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
                yield return new WaitForSeconds(0.1f); // adjust speed here
            }

        }

        //SceneManager.LoadScene("Tutorial MainRoom");
    }
}

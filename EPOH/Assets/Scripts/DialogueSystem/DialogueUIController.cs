using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    private void Awake()
    {
        //UI 보이지 않도록
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        Debug.Log("오픈 ui 실행");
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}

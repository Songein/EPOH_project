using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3Script : MonoBehaviour
{
    public GameObject dog_request_panel;
    
    void Start()
    {
        dog_request_panel.SetActive(false); // Dog 의뢰서 Panel 비활성화

    }

    public void activateRequestPanel()
    {
        dog_request_panel.SetActive(true);
    }
}

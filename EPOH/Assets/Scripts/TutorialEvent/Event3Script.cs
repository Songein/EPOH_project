using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3Script : MonoBehaviour
{
    public GameObject dog_request_panel;
    public GameObject character_panel; //Event 3 주인공 대화창 Panel

    void Start()
    {
        dog_request_panel.SetActive(false); // Dog 의뢰서 Panel 비활성화
        character_panel.SetActive(false);

    }

    public void activateRequestPanel()
    {
        dog_request_panel.SetActive(true);
    }
}

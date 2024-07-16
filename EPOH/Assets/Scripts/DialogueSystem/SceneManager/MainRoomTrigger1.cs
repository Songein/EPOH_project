using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainRoomTrigger1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (GameManager.instance.story_info)
            {
                case 7:
                    GameManager.instance.story_info = 8;
                    break;
            }
        }
    }
}

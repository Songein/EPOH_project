using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndArea : MonoBehaviour
{
    private BossDogScene scene_manager;
    private void Start()
    {
        scene_manager = FindObjectOfType<BossDogScene>();
    }
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scene_manager.EndTutorial();
        }
    }*/
}

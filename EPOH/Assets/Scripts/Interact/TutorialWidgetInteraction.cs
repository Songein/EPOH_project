using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWidgetInteraction : Interaction
{
    Event3Script event3Script;

    // PlayerSound audioSource
    private AudioSource audioSource;
    public AudioClip widgetSelectClip; // 코어 선택음

    
    void Start()
    {
        event3Script = GetComponent<Event3Script>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        event3Script.activateRequestPanel();
        widgetSelectSound();

    }

    void widgetSelectSound()
    {
        if (widgetSelectClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(widgetSelectClip);
        }
        else
        {
            Debug.LogWarning("widgetSelectClip이나 AudioSource가 null입니다.");
        }
    }
}

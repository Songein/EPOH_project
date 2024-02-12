using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : Interaction
{
    MoveToNextScene moveToNextScene;

    // PlayerSound audioSource
    private AudioSource audioSource;
    public AudioClip portalSelectClip; // 코어 선택음

    private void Start()
    {
        moveToNextScene = GetComponent<MoveToNextScene>();
        audioSource = GetComponent<AudioSource>();
       
    }
    public override void Interact()
    {
        moveToNextScene.sceneChange();
        portalSelectSound();
    }

    void portalSelectSound()
    {
        if (portalSelectClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(portalSelectClip);
        }
        else
        {
            Debug.LogWarning("portalSelectClip이나 AudioSource가 null입니다.");
        }
    }
}

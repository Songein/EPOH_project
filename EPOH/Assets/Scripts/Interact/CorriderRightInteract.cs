using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorriderRightInteract : Interaction
{
    EnterBossRoom enterBossRoom;

    // PlayerSound audioSource
    private AudioSource audioSource;
    public AudioClip portalSelectClip; // 코어 선택음
    
    private void Start()
    {
        enterBossRoom = GetComponent<EnterBossRoom>();
        audioSource = GetComponent<AudioSource>();
        
    }

    public override void Interact()
    {
       enterBossRoom.moveToBossRoom();
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

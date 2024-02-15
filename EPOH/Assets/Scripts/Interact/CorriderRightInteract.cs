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
    private Animator player_animator;
    
    private void Start()
    {
        enterBossRoom = GetComponent<EnterBossRoom>();
        audioSource = GetComponent<AudioSource>();
        player_animator = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    public override void Interact()
    {
        StartCoroutine(TransferStart());
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
    
    IEnumerator TransferStart()
    {
        //순간이동 시작 애니메이션
        player_animator.SetTrigger("TransferDeviceDepart");
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.teleport_depart = true;
        enterBossRoom.moveToBossRoom();
        portalSelectSound();
    }
    

}

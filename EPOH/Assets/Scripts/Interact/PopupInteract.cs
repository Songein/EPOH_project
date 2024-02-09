using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInteract : Interaction
{
    PopupController popupController;
    DefaultButtonSelection defaultButtonSelection;

    // PlayerSound audioSource
    private AudioSource audioSource;
    public AudioClip selectClip; // 코어 선택음

    private void Start()
    {
        popupController = GetComponent<PopupController>();
        defaultButtonSelection = GetComponent<DefaultButtonSelection>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        popupController.togglePopup();
        defaultButtonSelection.DefaultSetting();

        SelectSound();
    }

    void SelectSound()
    {
        if (selectClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(selectClip);
        }
        else
        {
            Debug.LogWarning("selectClip이나 AudioSource가 null입니다.");
        }
    }


}

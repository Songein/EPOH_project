using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortalInteract : Interaction
{
    MissionDecisionCorrider missionDecisionCorrider;
    MoveToNextScene moveToNextScene;
    PlayerController play_controller;

    private void Start()
    {
        missionDecisionCorrider = GetComponent<MissionDecisionCorrider>();
        moveToNextScene = GetComponent<MoveToNextScene>();
        play_controller = FindObjectOfType<PlayerController>();
    }

    public override void Interact()
    {
        if (missionDecisionCorrider != null && missionDecisionCorrider.canInteractWithPortal && moveToNextScene != null)
        {
            missionDecisionCorrider.onPortalInteraction();
            moveToNextScene.sceneChange();
        }

         // missionDecisionCorrider가 null이 아니고 canInteractWithPortal이 false일 때 is_interacting을 false로 설정
        if (missionDecisionCorrider != null && !missionDecisionCorrider.canInteractWithPortal && play_controller.is_interacting)
        { 
            play_controller.is_interacting = false;
        }

    }

   
}

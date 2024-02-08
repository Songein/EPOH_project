using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortalInteract : Interaction
{
    MissionDecisionCorrider missionDecisionCorrider;
    MoveToNextScene moveToNextScene;
    PlayerController player_controller;

    private void Start()
    {
        missionDecisionCorrider = GetComponent<MissionDecisionCorrider>();
        moveToNextScene = GetComponent<MoveToNextScene>();
        player_controller = FindObjectOfType<PlayerController>();
    }

    public override void Interact()
    {
        if (!missionDecisionCorrider.canInteractWithPortal)
        {
            player_controller.is_interacting = false;
        }
        else if (missionDecisionCorrider != null && moveToNextScene != null)
        {
            missionDecisionCorrider.onPortalInteraction();
            moveToNextScene.sceneChange();           
        }

    }

   
}

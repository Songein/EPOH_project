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

    private void Update()
    {
        // 플레이어가 상호작용 중이면서 Portal이 비활성화된 상태일때
        if (PlayerInteract.instance.is_interacting && !missionDecisionCorrider.canInteractWithPortal && missionDecisionCorrider.last_clicked_button == null)
        {
            PlayerInteract.instance.is_interacting = false;
        }
    }

    public override void Interact()
    {
        if (PlayerInteract.instance.is_interacting && !missionDecisionCorrider.canInteractWithPortal)
        {
            PlayerInteract.instance.is_interacting = false;
        }
        if (missionDecisionCorrider != null && missionDecisionCorrider.canInteractWithPortal && moveToNextScene != null)
        {
            missionDecisionCorrider.onPortalInteraction();
            moveToNextScene.sceneChange();
           
        }

    }

   
}

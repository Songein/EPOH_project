using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortalInteract : Interaction
{
    MissionDecisionCorrider missionDecisionCorrider;
    MoveToNextScene moveToNextScene;

    private void Start()
    {
        missionDecisionCorrider = GetComponent<MissionDecisionCorrider>();
        moveToNextScene = GetComponent<MoveToNextScene>();
    }

    public override void Interact()
    {
         if (missionDecisionCorrider != null)
        {
            missionDecisionCorrider.onPortalInteraction();
        }

        if (moveToNextScene != null)
        {
            moveToNextScene.sceneChange();
        }

    }
}

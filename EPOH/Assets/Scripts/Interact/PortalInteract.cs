using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : Interaction
{
    MoveToNextScene moveToNextScene;

    private void Start()
    {
        
        moveToNextScene = GetComponent<MoveToNextScene>();
       
    }
    public override void Interact()
    {
        moveToNextScene.sceneChange();
    }
}

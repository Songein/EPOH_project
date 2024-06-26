using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorriderRightInteract : Interaction
{
    EnterBossRoom enterBossRoom;
    
    private void Start()
    {
        enterBossRoom = GetComponent<EnterBossRoom>();
        
    }

    public override void Interact()
    {
       enterBossRoom.moveToBossRoom();
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomInteract : Interaction
{
    EnterBossRoom enterBossRoom;
    
    private void Start()
    {
        enterBossRoom = GetComponent<EnterBossRoom>();
        
    }

    public override void Interact()
    {
        enterBossRoom.setInteractingObjectName(this.gameObject.name);
        enterBossRoom.moveToBossRoom();
    }
    

}

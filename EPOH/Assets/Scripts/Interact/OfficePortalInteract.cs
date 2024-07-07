using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePortalInteract : Interaction
{
    MoveToOfficeRoom moveToOfficeRoom;
    
    private void Start()
    {
        moveToOfficeRoom = GetComponent<MoveToOfficeRoom>();
    }

    public override void Interact()
    {
        moveToOfficeRoom.setInteractingObjectName(this.gameObject.name);
        moveToOfficeRoom.officeSceneChange();
    }
}

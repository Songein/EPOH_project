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
        moveToOfficeRoom.officeSceneChange();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeElevatorInteract : Interaction
{
    MoveFromElevator moveFromElevator;

    void Start()
    {
        moveFromElevator = GetComponent<MoveFromElevator>();
    }

    public override void Interact()
    {
        moveFromElevator.operateElevator();
    }
}

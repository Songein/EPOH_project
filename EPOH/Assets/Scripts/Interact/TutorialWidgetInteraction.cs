using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWidgetInteraction : Interaction
{
    Event3Script event3Script;

    
    void Start()
    {
        event3Script = GetComponent<Event3Script>();
    }

    public override void Interact()
    {
        event3Script.activateRequestPanel();
    }
}

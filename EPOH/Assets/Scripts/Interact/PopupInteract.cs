using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInteract : Interaction
{
    PopupController popupController;

    private void Start()
    {
        popupController = GetComponent<PopupController>();
    }

    public override void Interact()
    {
        popupController.togglePopup();
    }

}

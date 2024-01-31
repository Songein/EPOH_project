using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInteract : Interaction
{
    PopupController popupController;
    DefaultButtonSelection defaultButtonSelection;

    private void Start()
    {
        popupController = GetComponent<PopupController>();
        defaultButtonSelection = GetComponent<DefaultButtonSelection>();
    }

    public override void Interact()
    {
        popupController.togglePopup();
        defaultButtonSelection.DefaultSetting();
    }

}

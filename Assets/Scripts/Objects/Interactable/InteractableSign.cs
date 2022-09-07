using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableSign : InteractableBase
{
    public Dialog CurrentDialog;
    public string Dialog;

    void FixedUpdate()
    {
        UpdateInteract();
    }

    public override void InteractInternal()
    {
        if (CurrentDialog.IsActive)
        {
            CurrentDialog.Close();
        }
        else
        {
            CurrentDialog.ShowText(Dialog);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerExit2D(other);
            CurrentDialog.Close();
        }
    }
}

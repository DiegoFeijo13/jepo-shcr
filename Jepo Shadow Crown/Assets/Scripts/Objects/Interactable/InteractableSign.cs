using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableSign : InteractableBase
{
    public Dialog CurrentDialog;
    public string Dialog;

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Action") && PlayerInRange)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSign : InteractableBase
{
    public string Text;
    public override void OnInteract()
    {
        if (DialogBox.IsVisible())
        {
            DialogBox.Hide();
        }
        else
        {
            DialogBox.Show(Text);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChest : InteractableBase
{
    bool _isOpen;
    public Sprite OpenChestSprite;
    public override void OnInteract()
    {
        if (OpenChestSprite != null)
        {
            if (_isOpen)
                return;

            GetComponentInChildren<SpriteRenderer>().sprite = OpenChestSprite;
            _isOpen = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class InteractableDoor : InteractableBase
{
    [Header("Door variables")]
    public DoorType ThisDoorType;
    public bool IsOpen = false;
    public Inventory PlayerInventory;
    public GameObject DoorVisuals;

    void Update()
    {
        UpdateInteract();
    }

    public override void InteractInternal()
    {
        if (ThisDoorType == DoorType.key && PlayerInventory.Keys > 0)
        {
            PlayerInventory.Keys--;
            Open();
        }
    }

    public void Open()
    {
        DoorVisuals.SetActive(false);     
        IsOpen = true;
        Context.Raise();
    }

    public void Close()
    {
        DoorVisuals.SetActive(true);
        IsOpen = false;        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsOpen)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!IsOpen)
        {
            base.OnTriggerExit2D(other);
        }
    }
}

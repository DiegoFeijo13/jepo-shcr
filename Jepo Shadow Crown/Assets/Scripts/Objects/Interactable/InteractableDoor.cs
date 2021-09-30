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
        if (Input.GetButtonDown("Action") && PlayerInRange)
        {
            if(ThisDoorType == DoorType.key && PlayerInventory.Keys > 0)
            {
                PlayerInventory.Keys--;
                Open();
            }
        }
    }

    public void Open()
    {
        DoorVisuals.SetActive(false);     
        IsOpen = true;                
    }

    public void Close()
    {
        DoorVisuals.SetActive(true);
        IsOpen = false;        
    }
}

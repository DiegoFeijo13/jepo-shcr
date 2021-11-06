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
    [SerializeField] private DoorType thisDoorType;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject doorVisuals;

    public DoorType Type => thisDoorType;

    void Update()
    {
        UpdateInteract();
    }

    private void Open()
    {
        if (thisDoorType == DoorType.key)
        {
            if (playerInventory.Keys <= 0)
                return;
            
            playerInventory.Keys--;
        }

        doorVisuals.SetActive(false);     
        isOpen = true;
        playerInRange = false;
        UpdateContextClue();
    }

    public override void InteractInternal()
    {
        if (!isOpen)
            Open();
    }

    public void Close()
    {
        doorVisuals.SetActive(true);
        isOpen = false;        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!isOpen)
        {
            base.OnTriggerExit2D(other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableChest : InteractableBase
{
    [Header("Contents")]
    [SerializeField] private Item contents;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private bool isOpen;

    [Header("Animation")]
    private Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    
        if (isOpen)
        {
            anim.SetBool("isOpen", true);
        }
    }

    void Update()
    {
        UpdateInteract();
    }

    public override void InteractInternal()
    {
        if (!isOpen)
        {
            //Open the chest
            OpenChest();
        }
        else
        {
            //Chest is already open
            ChestAlreadyOpen();
        }
    }

    public void OpenChest()
    {
        playerInventory.CurrentItem = contents;
        playerInventory.AddItem(contents);
        isOpen = true;
        playerInRange = false;
        anim.SetBool("isOpen", true);
        UpdateContextClue();
    }

    public void ChestAlreadyOpen()
    {
       
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

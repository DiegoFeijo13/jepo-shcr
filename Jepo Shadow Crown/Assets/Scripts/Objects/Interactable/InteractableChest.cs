using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableChest : InteractableBase
{
    [Header("Contents")]
    public Item Contents;
    public Inventory PlayerInventory;
    public bool IsOpen;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    
        if (IsOpen)
        {
            anim.SetBool("isOpen", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInteract();
    }

    public override void InteractInternal()
    {
        if (!IsOpen)
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
        PlayerInventory.CurrentItem = Contents;
        PlayerInventory.AddItem(Contents);
        IsOpen = true;
        anim.SetBool("isOpen", true);
    }

    public void ChestAlreadyOpen()
    {
       
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

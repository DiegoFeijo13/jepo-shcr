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
    //public BoolValue StoredOpen;

    //[Header("Signals and Dialog")]
    //public SignalSender raiseItem;
    //public GameObject dialogBox;
    //public Text dialogText;

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
        //Dialog window on
        //dialogBox.SetActive(true);
        //Dialog text = contents text
        //dialogText.text = Contents.ItemDescription;
        //Add contents to the inventory
        PlayerInventory.CurrentItem = Contents;
        PlayerInventory.AddItem(Contents);
        //Raise the signal to the player to animate
        //raiseItem.Raise();
        //Raise the context clue
        Context.Raise();        
        IsOpen = true;
        anim.SetBool("isOpen", true);
        //StoredOpen.RunTimeValue = IsOpen;
    }

    public void ChestAlreadyOpen()
    {
        //Dialog off
        //dialogBox.SetActive(false);
        //Raise the signal to the player to stop animating
        //raiseItem.Raise();
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

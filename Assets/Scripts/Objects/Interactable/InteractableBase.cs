using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{    
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject contextClue;
    
    protected bool playerInRange;

    private void Awake()
    {
        UpdateContextClue();
    }

    protected void UpdateContextClue()
    {
        if (contextClue == null)
            return;

        contextClue.SetActive(playerInRange);
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {            
            playerInRange = true;
            UpdateContextClue();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {            
            playerInRange = false;
            UpdateContextClue();
        }
    }

    public void UpdateInteract()
    {
        if (Input.GetButtonDown("Action") && playerInRange)
        {
            InteractInternal();
        }
    }

    public virtual void InteractInternal()
    {
        Debug.LogError("Not implemented!");
    }

}

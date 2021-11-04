using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{    
    protected bool PlayerInRange;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {            
            PlayerInRange = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {            
            PlayerInRange = false;
        }
    }

    public void UpdateInteract()
    {
        if (Input.GetButtonDown("Action") && PlayerInRange)
        {
            InteractInternal();
        }
    }

    public virtual void InteractInternal()
    {
        Debug.LogError("Not implemented!");
    }

}

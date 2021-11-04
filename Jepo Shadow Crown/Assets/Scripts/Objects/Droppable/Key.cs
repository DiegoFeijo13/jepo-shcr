using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : DroppableItem
{
    public Inventory PlayerInventory;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInventory.Keys++;
            Destroy(this.gameObject);
        }
    }
}

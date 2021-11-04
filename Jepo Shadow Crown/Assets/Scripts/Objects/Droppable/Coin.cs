using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DroppableItem
{
    public Inventory PlayerInventory;
    public int Amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInventory.Coins += Amount;
            Destroy(this.gameObject);
        }
    }
}

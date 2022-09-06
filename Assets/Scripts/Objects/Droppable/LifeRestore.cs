using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRestore : DroppableItem
{
    public PlayerHealth PlayerHealth;
    public float amountToIncrease;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerHealth.RestoreHealth(amountToIncrease);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{    
    public PlayerMovement PlayerMovement;    
    public CharacterInventoryModel Inventory;
    public PlayerView PlayerView;
    public PlayerHealth Health;

    public void DealDamage(int damage, bool isCritical)
    {
        Health.DealDamage(damage);
        PlayerView.TakeHit(damage, isCritical);
    }
}

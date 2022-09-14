using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRestore : DroppableItem
{
    [SerializeField] private PlayerHealth PlayerHealth;
    [SerializeField] private int minToRestore;
    [SerializeField] private int maxToRestore;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            int restore = Calculator.CalculateRestoreHealth(minToRestore, maxToRestore);
            PlayerHealth.RestoreHealth(restore);

            TextPopup.ShowHeal(restore, transform.position);

            Destroy(this.gameObject);
        }
    }
}

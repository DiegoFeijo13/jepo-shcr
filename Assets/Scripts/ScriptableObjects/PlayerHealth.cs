using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerHealth : ScriptableObject
{
    public float StartingHealth;

    public float Health { get; private set; }
    public float MaxHealth { get; private set; }

    public void OnEnable()
    {
        MaxHealth = StartingHealth;
        Health = StartingHealth;
    }

    public void DealDamage(float damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            GameManager.Instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (amount <= 0)
            return;

        if (Health >= MaxHealth)
            return;

        Health += amount;

        if (Health >= MaxHealth)
            Health = MaxHealth;
    }
}

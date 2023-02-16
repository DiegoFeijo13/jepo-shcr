using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private float lifetime;

    private float lifetimeCounter;
    private int minDamage, maxDamage;

    void Start()
    {
        lifetimeCounter = lifetime;
    }

    void FixedUpdate()
    {
        lifetimeCounter -= Time.deltaTime;
        if (lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    internal void SetDamage(int minDamage, int maxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls") || collision.gameObject.CompareTag("Objects"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            var damage = Calculator.CalculateDamage(minDamage, maxDamage);
            collision.gameObject.GetComponent<Character>().DealDamage(damage, Calculator.IsLastRollCritical);
            
            Destroy(this.gameObject);
        }
    }
}

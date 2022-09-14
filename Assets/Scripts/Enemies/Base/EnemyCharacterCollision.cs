using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour
{
    EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            enemyBase.OnAttack(collider.gameObject);
        }
    }
}

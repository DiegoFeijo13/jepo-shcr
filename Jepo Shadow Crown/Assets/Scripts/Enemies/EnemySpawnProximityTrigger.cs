using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnProximityTrigger : MonoBehaviour
{
    EnemySpawn control;

    private void Awake()
    {
        control = GetComponentInParent<EnemySpawn>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            control.SetCharacterInRange(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            control.SetCharacterInRange(null);
        }
    }
}

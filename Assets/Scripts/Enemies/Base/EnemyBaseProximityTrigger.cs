using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseProximityTrigger : MonoBehaviour
{
    EnemyBase control;

    private void Awake()
    {
        control = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            control.CharacterInRange = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            control.CharacterInRange = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour
{
    CharacterBatControl _batControl;

    private void Awake()
    {
        _batControl = GetComponentInParent<CharacterBatControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _batControl.OnHitCharacter(collider.gameObject);
        }
    }
}

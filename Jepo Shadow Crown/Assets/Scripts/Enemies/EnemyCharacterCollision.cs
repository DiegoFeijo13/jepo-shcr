using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour
{
    SkeletonControl _skeletonControl;

    private void Awake()
    {
        _skeletonControl = GetComponentInParent<SkeletonControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _skeletonControl.OnHitCharacter(collider.gameObject);
        }
    }
}

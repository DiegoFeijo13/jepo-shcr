using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProximityTrigger : MonoBehaviour
{
    SkeletonControll _control;

    private void Awake()
    {
        _control = GetComponentInParent<SkeletonControll>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {        
        if(collider.CompareTag("Player"))
        {
            _control.SetCharacterInRange(collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _control.SetCharacterInRange(null);
        }
    }
}

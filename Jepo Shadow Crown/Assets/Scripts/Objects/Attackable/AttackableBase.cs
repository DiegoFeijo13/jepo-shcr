using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableBase : MonoBehaviour
{
    public virtual void OnHit(Collider2D hitCollider, ItemType itemType)
    {
        Debug.LogWarning("No OnHit method setup for" + gameObject.name, gameObject);
    }

    public virtual void OnHit(Vector2 pushDirection, ItemType itemType, float damage)
    {
        Debug.LogWarning("No OnHit method setup for" + gameObject.name, gameObject);
    }
}

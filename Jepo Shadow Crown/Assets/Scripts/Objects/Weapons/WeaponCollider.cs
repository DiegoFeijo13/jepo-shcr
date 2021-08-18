using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    public ItemType Type;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        AttackableBase attackable = collider.gameObject.GetComponent<AttackableBase>();

        if (attackable != null)
        {
            attackable.OnHit(_collider, Type);
        }
    }
}

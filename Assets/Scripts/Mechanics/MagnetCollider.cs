using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollider : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DroppableItem item))
        {
            item.SetTargetPosition(transform.position);
        }
    }
}

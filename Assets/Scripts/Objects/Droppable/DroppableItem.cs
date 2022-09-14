using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItem : MonoBehaviour
{
    [SerializeField] protected bool isAttractable;

    private Vector3 targetPosition;
    private bool hasTarget;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (hasTarget && isAttractable)
        {
            var targetDir = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDir.x, targetDir.y) * 5f;
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        hasTarget = true;
    }
}

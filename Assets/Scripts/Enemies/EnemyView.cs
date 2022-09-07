﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement movement;

    private void Awake()
    {
        if (animator == null)
        {
            Debug.LogError("Character Animator not setup!");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        animator.SetBool("isMoving", movement.IsMoving());
    }

    public virtual void DoAttack()
    {
        animator.SetTrigger("doAttack");
    }
}

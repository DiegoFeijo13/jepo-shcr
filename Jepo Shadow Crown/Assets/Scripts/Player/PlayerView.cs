using Assets.Scripts.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{    
    [SerializeField] private GameObject visuals;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D triggerCollider;

    [Header("IFrame")]
    [SerializeField] private float flashDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRenderer;
    private Color defaultcolor;
    private PlayerMovement movementModel;

    private void Awake()
    {
        movementModel = GetComponent<PlayerMovement>();
        spriteRenderer = visuals.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            defaultcolor = spriteRenderer.color;

        if (animator == null)
        {
            Debug.LogError("Character Animator not setup!");
            enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Vector3 direction = movementModel.GetDirection();

        if (direction != Vector3.zero)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }

        animator.SetBool("isMoving", movementModel.IsMoving());
    }

    public void TakeHit()
    {
        StartCoroutine(TakeHitCo());
    }


    private IEnumerator TakeHitCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;

        while (temp < numberOfFlashes)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
    
}

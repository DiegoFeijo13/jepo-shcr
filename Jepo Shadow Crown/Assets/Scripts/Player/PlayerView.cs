using Assets.Scripts.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : BaseMovementView
{    
    public Transform WeaponParent;
    public GameObject _visuals;
    public GameObject Projectile;

    private PlayerControl _movementModel;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _movementModel = GetComponent<PlayerControl>();        

        if (Animator == null)
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
        Vector3 facingDirection = _movementModel.GetFacingDirection();
        if ((facingDirection.x == 1 && !_isFacingRight) || (facingDirection.x == -1 && _isFacingRight))
            Flip();

        Vector3 direction = _movementModel.GetDirection();

        if (direction != Vector3.zero)
        {
            Animator.SetFloat("moveX", direction.x);
            Animator.SetFloat("moveY", direction.y);
        }

        Animator.SetBool("isMoving", _movementModel.IsMoving());
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = _visuals.transform.localScale;
        scale.x *= -1;
        _visuals.transform.localScale = scale;
    }

    private IEnumerator AttackCo()
    {
        _movementModel.CurrentState = MovementState.attacking;
        //Animator.SetBool("isAttacking", true);
        yield return null;
        //MakeBullet();
        //Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        if (_movementModel.CurrentState != MovementState.interacting)
        {
            _movementModel.CurrentState = MovementState.idle;
        }
    }

    public void DoSecondAttack()
    {
        StartCoroutine(SecondAttackCo());
    }

    private IEnumerator SecondAttackCo()
    {
        _movementModel.CurrentState = MovementState.attacking;
        Animator.SetBool("isAttacking", true);
        yield return null;
        Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        if (_movementModel.CurrentState != MovementState.interacting)
        {
            _movementModel.CurrentState = MovementState.idle;
        }
    }
}

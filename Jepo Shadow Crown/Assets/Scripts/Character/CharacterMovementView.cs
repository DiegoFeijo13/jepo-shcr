using Assets.Scripts.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementView : BaseMovementView
{    
    public Transform WeaponParent;
    public GameObject _visuals;
    public GameObject Projectile;

    private CharacterMovementModel _movementModel;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _movementModel = GetComponent<CharacterMovementModel>();        

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

    public override void DoAttack()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        _movementModel.SetState(MovementState.attacking);
        Animator.SetBool("isAttacking", true);
        yield return null;
        Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        if(_movementModel.GetState() != MovementState.interacting)
        {
            _movementModel.SetState(MovementState.idle);
        }
    }

    public void DoSecondAttack()
    {
        StartCoroutine(SecondAttackCo());
    }

    private IEnumerator SecondAttackCo()
    {
        _movementModel.SetState(MovementState.attacking);
        //Animator.SetBool("isAttacking", true);
        yield return null;
        MakeBullet();
        //Animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        if (_movementModel.GetState() != MovementState.interacting)
        {
            _movementModel.SetState(MovementState.idle);
        }
    }

    private void MakeBullet()
    {
        Vector2 temp = new Vector2(Animator.GetFloat("moveX"), Animator.GetFloat("moveY"));
        Bullet arrow = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Bullet>();
        arrow.Setup(temp, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(Animator.GetFloat("moveY"), Animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
}

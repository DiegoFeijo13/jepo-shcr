using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementView : MonoBehaviour
{
    public Animator Animator;
    public Transform WeaponParent;
    public GameObject _visuals;

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

        Animator.SetBool("isMoving", _movementModel.IsMoving());
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = _visuals.transform.localScale;
        scale.x *= -1;
        _visuals.transform.localScale = scale;
    }

    public void DoAttack()
    {
        Animator.SetTrigger("doAttack");
    }

    public void OnAttackStart()
    {
        SetWeaponActive(true);
    }

    public void OnAttackStop()
    {
        SetWeaponActive(false);
    }

    void SetWeaponActive(bool doActivate)
    {
        //for (int i = 0; i < WeaponParent.childCount; i++)
        //{
        //    WeaponParent.GetChild(i).gameObject.SetActive(doActivate);
        //}
    }
}

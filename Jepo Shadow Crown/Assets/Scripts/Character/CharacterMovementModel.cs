using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour
{
    public float Speed;

    private Vector3 _movementDirection;
    private Vector3 _facingDirection;
    private Rigidbody2D _body;
    private bool _isFrozen;
    private bool _isAttacking;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    
    void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (_isFrozen)
        {
            _body.velocity = Vector2.zero;
            return;
        }

        if (_movementDirection != Vector3.zero)
            _movementDirection.Normalize();

        _body.velocity = _movementDirection * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        if (_isFrozen)
            return;

        _movementDirection = new Vector3(direction.x, direction.y, 0);
        if (direction != Vector2.zero)
            _facingDirection = _movementDirection;
    }

    public Vector3 GetDirection()
    {
        return _movementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return _facingDirection;
    }

    public bool IsMoving()
    {
        return _movementDirection != Vector3.zero;
    }

    public bool IsFrozen()
    {
        return _isFrozen;
    }

    public void SetFrozen(bool isFrozen)
    {
        _isFrozen = isFrozen;
    }

    public bool CanAttack()
    {
        if (_isAttacking || IsMoving())
            return false;
        return true; ;
    }

    public void DoAttack()
    {
        
    }

    public void OnAttackStart()
    {
        _isAttacking = true;
    }

    public void OnAttackStop()
    {
        _isAttacking = false;
    }
}

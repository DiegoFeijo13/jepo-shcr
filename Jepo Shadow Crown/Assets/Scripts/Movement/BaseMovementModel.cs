using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    idle,
    walking,
    interacting,
    attacking,
    frozen,
    staggering,
    auto,
}

public class BaseMovementModel : MonoBehaviour
{
    public float Speed;

    protected Vector3 _movementDirection;
    protected Vector3 _facingDirection;

    protected Rigidbody2D _body;

    [HideInInspector]
    public MovementState CurrentState { get; set; }

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(CurrentState != MovementState.auto)
            UpdateMovement();
    }

    protected virtual void UpdateMovement()
    {
        if (CurrentState == MovementState.frozen)
        {
            _body.velocity = Vector2.zero;
            return;
        }

        if (_movementDirection != Vector3.zero && CurrentState != MovementState.staggering)
        {
            _movementDirection.Normalize();
            CurrentState = MovementState.walking;
        }
        
        if (_movementDirection == Vector3.zero)
        {
            CurrentState = MovementState.idle;
        }

        if (CurrentState == MovementState.walking)
        { 
            _body.velocity = _movementDirection * Speed;
        }
        else
        {
            _body.velocity = Vector2.zero;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (CurrentState == MovementState.frozen || CurrentState == MovementState.attacking)
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
        return CurrentState == MovementState.frozen;
    }

    public void SetFrozen(bool isFrozen, bool affectGameTime)
    {
        if (isFrozen)
            CurrentState = MovementState.frozen;
        else
            CurrentState = MovementState.idle;

        if (affectGameTime)
        {
            if (isFrozen)
            {
                StartCoroutine(FreezeTimeCo());
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator FreezeTimeCo()
    {
        //Wait 1 frame
        yield return null;

        Time.timeScale = 0f;
    }

    public bool CanAttack()
    {
        List<MovementState> blockedStates = new List<MovementState>
        {
            MovementState.attacking,
            MovementState.frozen
        };

        return !blockedStates.Contains(CurrentState);
    }

    public void AutoMove(Vector2 dir, float seconds)
    {
        CurrentState = MovementState.auto;
        _body.velocity = dir * Speed;
        StartCoroutine(AutoMovementCO(seconds));
    }

    IEnumerator AutoMovementCO(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CurrentState = MovementState.idle;
    }
}

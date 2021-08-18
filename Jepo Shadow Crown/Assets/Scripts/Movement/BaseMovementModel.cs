using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    idle,
    walking,
    interacting,
    attacking,
    frozen
}

public class BaseMovementModel : MonoBehaviour
{
    public float Speed;    

    protected Vector3 _movementDirection;
    protected Vector3 _facingDirection;

    protected Rigidbody2D _body;

    protected MovementState _currentState;

    protected Vector2 _pushDirection;
    protected float _pushTime;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdatePushTime();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    protected virtual void UpdateMovement()
    {
        if (_currentState == MovementState.frozen || _currentState == MovementState.attacking)
        {
            _body.velocity = Vector2.zero;
            return;
        }

        if (_movementDirection != Vector3.zero)
        {
            _movementDirection.Normalize();
            _currentState = MovementState.walking;
        }
        else if (_currentState == MovementState.walking)
        {
            _currentState = MovementState.idle;
        }

        if (IsBeingPushed())
        {
            _body.velocity = _pushDirection;
        }
        else
        {
            _body.velocity = _movementDirection * Speed;
        }
    }

    protected void UpdatePushTime()
    {
        _pushTime = Mathf.MoveTowards(_pushTime, 0f, Time.deltaTime);
    }

    protected bool IsBeingPushed()
    {
        return _pushTime > 0;
    }

    public void SetDirection(Vector2 direction)
    {
        if (_currentState == MovementState.frozen || _currentState == MovementState.attacking)
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
        return _currentState == MovementState.frozen;
    }

    public void SetFrozen(bool isFrozen, bool affectGameTime)
    {
        if (isFrozen)
            _currentState = MovementState.frozen;
        else
            _currentState = MovementState.idle;        

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
            MovementState.frozen,
            MovementState.walking
        };

        return !blockedStates.Contains(_currentState);
    }

    public void PushCharacter(Vector2 pushDirection, float time)
    {
        _pushDirection = pushDirection;
        _pushTime = time;
    }
}

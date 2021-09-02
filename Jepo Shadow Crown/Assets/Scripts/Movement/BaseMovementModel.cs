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
}

public class BaseMovementModel : MonoBehaviour
{
    public float Speed;

    protected Vector3 _movementDirection;
    protected Vector3 _facingDirection;

    protected Rigidbody2D _body;


    protected Vector2 _pushDirection;
    protected float _pushTime;

    [HideInInspector]
    public MovementState CurrentState { get; set; }

    private void Start()
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
        if (CurrentState == MovementState.frozen || CurrentState == MovementState.attacking)
        {
            _body.velocity = Vector2.zero;
            return;
        }

        if (_movementDirection != Vector3.zero)
        {
            _movementDirection.Normalize();
            CurrentState = MovementState.walking;
        }
        else if (CurrentState == MovementState.walking)
        {
            CurrentState = MovementState.idle;
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

    public void PushCharacter(Vector2 pushDirection, float time)
    {
        _pushDirection = pushDirection;
        _pushTime = time;
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (_body != null)
        {
            yield return new WaitForSeconds(knockTime);
            _body.velocity = Vector2.zero;
            CurrentState = MovementState.idle;            
        }
    }
}

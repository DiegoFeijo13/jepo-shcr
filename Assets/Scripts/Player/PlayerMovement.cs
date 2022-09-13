using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Enums;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 movementDirection;
    private Vector3 facingDirection;
    private Rigidbody2D body;
    private PlayerState currentState;
    private Vector2 movement;

    public Vector2 BodyPos => body.position;

    private void Awake()
    {
        SetDirection(Vector2.zero);

        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement == Vector2.zero)
            SetDirection(Vector2.zero);
        else
            SetDirection(movement);

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (currentState == PlayerState.frozen)
        {
            body.velocity = Vector2.zero;
            return;
        }

        if (movementDirection == Vector3.zero)
        {
            currentState = PlayerState.idle;
        }
        else
        {
            movementDirection.Normalize();
            currentState = PlayerState.walking;
        }

        if (currentState == PlayerState.walking)
        {
            body.velocity = movementDirection * speed;
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    #region Public Methods
    public void SetDirection(Vector2 direction)
    {
        if (currentState == PlayerState.frozen || currentState == PlayerState.attacking)
            return;

        movementDirection = new Vector3(direction.x, direction.y, 0);
        if (direction != Vector2.zero)
            facingDirection = movementDirection;
    }

    public Vector3 GetDirection()
    {
        return movementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }

    public bool IsMoving()
    {
        return movementDirection != Vector3.zero;
    }

    public bool IsFrozen()
    {
        return currentState == PlayerState.frozen;
    }

    public void SetFrozen(bool isFrozen, bool affectGameTime)
    {
        if (isFrozen)
            currentState = PlayerState.frozen;
        else
            currentState = PlayerState.idle;

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

    public bool CanAttack()
    {
        List<PlayerState> blockedStates = new List<PlayerState>
        {
            PlayerState.attacking,
            PlayerState.frozen
        };

        return !blockedStates.Contains(currentState);
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }

    IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    #endregion Public Methods

    #region Coroutines
    IEnumerator FreezeTimeCo()
    {
        //Wait 1 frame
        yield return null;

        Time.timeScale = 0f;
    }
    #endregion Coroutines
}

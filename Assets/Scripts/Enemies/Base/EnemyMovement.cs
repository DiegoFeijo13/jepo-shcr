using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    protected EnemyState CurrentState { get; set; }
    protected Vector3 movementDirection;
    protected Vector3 facingDirection;

    private Rigidbody2D body;

    public float GetSpeed => speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetDirection(movementDirection);

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (CurrentState == EnemyState.frozen)
        {
            body.velocity = Vector2.zero;
            return;
        }

        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize();
            CurrentState = EnemyState.walking;
        }

        if (movementDirection == Vector3.zero)
        {
            CurrentState = EnemyState.idle;
        }

        if (CurrentState == EnemyState.walking)
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
        if (CurrentState == EnemyState.frozen || CurrentState == EnemyState.attacking)
            return;

        movementDirection = new Vector3(direction.x, direction.y, 0);
        if (direction != Vector2.zero)
            facingDirection = movementDirection;
    }

    public Vector3 GetDirection()
    {
        return movementDirection;
    }

    public bool IsMoving()
    {
        return movementDirection != Vector3.zero;
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }
    #endregion Public Methods

    #region Coroutines
    IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            CurrentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    #endregion Coroutines
}
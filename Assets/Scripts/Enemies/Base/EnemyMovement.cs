using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider2D bounds;
    [SerializeField] private float minMoveTime = 1f;
    [SerializeField] private float maxMoveTime = 1.75f;
    [SerializeField] private EnemyMovementType inRangeMovementType = EnemyMovementType.chasePlayer;
    [SerializeField] private EnemyMovementType outOfRangeMovementType = EnemyMovementType.random;

    protected EnemyState CurrentState { get; set; }
    protected Vector3 movementDirection;
    protected Vector3 facingDirection;

    private Rigidbody2D body;
    private Vector2 directionVector;

    //private float moveTimeSeconds;
    private float waitTimeSeconds;

    protected EnemyBase enemyBase;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        enemyBase = GetComponent<EnemyBase>();
    }

    private void Start()
    {
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        ChangeDirection();
    }

    protected void FixedUpdate()
    {
        SetDirectionInternal(movementDirection);

        UpdateMovement();
        UpdateDirection();
    }

    void UpdateDirection()
    {
        waitTimeSeconds -= Time.deltaTime;
        if (enemyBase.CanMove())
        {
            if (enemyBase.CharacterInRange == null)
            {
                if (waitTimeSeconds > 0)
                    return;

                EnemyOutOfRangeMovement();
            }
            else
                EnemyInRangeMovement();

        }
        else
        {
            directionVector = Vector2.zero;
        }

        if (waitTimeSeconds <= 0)
            waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);

        SetDirection(directionVector);
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

    private void ChooseDifferentDirection()
    {
        Vector2 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        directionVector = directions[Random.Range(0, directions.Length)];
    }

    protected void SetDirectionInternal(Vector2 direction)
    {
        if (!enemyBase.CanMove())
            return;

        movementDirection = new Vector3(direction.x, direction.y, 0);
        if (direction != Vector2.zero)
            facingDirection = movementDirection;
    }

    private void SetDirection(Vector2 direction)
    {
        if (bounds != null)
        {
            Vector2 temp = (Vector2)transform.position + speed * Time.deltaTime * direction;
            if (!bounds.bounds.Contains(temp))
            {
                ChooseDifferentDirection();
                return;
            }
        }
        SetDirectionInternal(direction);
    }

    protected virtual void EnemyOutOfRangeMovement()
    {
        switch (outOfRangeMovementType)
        {
            case EnemyMovementType.chasePlayer:
                ChasePlayer();
                break;
            case EnemyMovementType.awayFromPlayer:
                AwayFromPlayer();
                break;
            case EnemyMovementType.random:
            default:
                RandomMovement();
                break;
        }
    }

    protected virtual void EnemyInRangeMovement()
    {
        switch (inRangeMovementType)
        {
            case EnemyMovementType.chasePlayer:
                ChasePlayer();
                break;
            case EnemyMovementType.awayFromPlayer:
                AwayFromPlayer();
                break;
            case EnemyMovementType.random:
            default:
                RandomMovement();
                break;
        }
    }

    #region Predefined IA Movements
    protected void RandomMovement()
    {
        ChooseDifferentDirection();
    }

    protected void ChasePlayer()
    {
        directionVector = enemyBase.CharacterInRange.transform.position - transform.position;
        directionVector.Normalize();
    }

    protected void AwayFromPlayer()
    {
        directionVector = enemyBase.CharacterInRange.transform.position + transform.position;
        directionVector.Normalize();
    }
    #endregion

    #region Public Methods
    internal bool IsMoving() => movementDirection != Vector3.zero;
    #endregion Public Methods

}
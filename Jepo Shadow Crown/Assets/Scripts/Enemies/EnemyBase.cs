using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : AttackableBase
{
    public int MaxHealth;
    public GameObject MainObject;
    public float HitPushStrength;
    public float HitPushDuration;
    public float DestroyDelayOnDeath;
    public GameObject DeathFX;
    public float DelayDeathFX;
    public float DamagePerHit;
    public BoxCollider2D Bounds;

    protected float _health;

    protected BaseMovementModel _movementModel;
    protected Transform _transform;

    protected Vector3 _startPos;
    protected Vector2 _directionVector;

    [HideInInspector]
    public MovementState CurrentState { get; set; }

    private void Awake()
    {
        _movementModel = GetComponent<BaseMovementModel>();
        _transform = GetComponent<Transform>();

        CurrentState = MovementState.idle;
        _health = MaxHealth;
        if (MainObject != null)
        {
            _startPos = MainObject.transform.position;
        }
    }


    protected void SetDirection(Vector2 direction)
    {
        if (_movementModel == null)
            return;
        if (Bounds != null)
        {
            Vector2 temp = (Vector2)_transform.position + direction * _movementModel.Speed * Time.deltaTime;
            if (!Bounds.bounds.Contains(temp))
            {
                ChooseDifferentDirection();
                return;
            }
        }

        _movementModel.SetDirection(direction);
    }

    protected void ChooseDifferentDirection()
    {
        Vector2 temp = _directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == _directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    protected void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                _directionVector = Vector2.right;
                break;
            case 1:
                _directionVector = Vector2.up;
                break;
            case 2:
                _directionVector = Vector2.left;
                break;
            case 3:
                _directionVector = Vector2.down;
                break;
            default:
                break;
        }
    }

    public override void OnHit(Vector2 pushDirection, ItemType itemType, float damage)
    {
        _health -= damage;

        if (_movementModel != null)
        {
            pushDirection = pushDirection.normalized * HitPushStrength;

            //_movementModel.PushCharacter(pushDirection, HitPushDuration);
        }

        if (_health <= 0)
        {
            StartCoroutine(DestroyCo(DestroyDelayOnDeath));

            if (DeathFX != null)
            {
                StartCoroutine(DeathFXCo(DelayDeathFX));
            }
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));

    }

    public void FullRestore()
    {
        MainObject.SetActive(true);
        MainObject.transform.position = _startPos;
        _health = MaxHealth;
        CurrentState = MovementState.idle;
    }

    #region Coroutines
    IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            CurrentState = MovementState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    IEnumerator DestroyCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        MainObject.SetActive(false);

        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator DeathFXCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(DeathFX, transform.position, Quaternion.identity);
    }
    #endregion Coroutines
}

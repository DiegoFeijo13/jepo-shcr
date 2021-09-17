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

    protected float _health;

    protected BaseMovementModel _movementModel;

    protected Vector3 _startPos;

    [HideInInspector]
    public MovementState CurrentState { get; set; }

    private void Awake()
    {
        _movementModel = GetComponent<BaseMovementModel>();
        CurrentState = MovementState.idle;
        _health = MaxHealth;
        if (MainObject != null)
        {
            _startPos = MainObject.transform.position;
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            CurrentState = MovementState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    protected void SetDirection(Vector2 direction)
    {
        if (_movementModel == null)
            return;

        _movementModel.SetDirection(direction);
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
}

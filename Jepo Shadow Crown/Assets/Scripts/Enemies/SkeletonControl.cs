using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControl : EnemyBase
{
    GameObject characterInRange;
     
    [SerializeField] private float minWaitTime = 0.1f;
    [SerializeField] private float maxWaitTime = 0.75f;

    private float moveTimeSeconds;
    private float minMoveTime = 1f;
    private float maxMoveTime = 1.75f;
    private float waitTimeSeconds;

    private void Start()
    {
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        ChangeDirection();
    }

    private void Update()
    {
        UpdateDirection();
    }

    protected override void OnDie()
    {
        EnemyScore.UpdateKills(Enemies.Skeleton);

        base.OnDie();
    }

    void UpdateDirection()
    {
        if (CurrentState == EnemyState.walking)
        {
            moveTimeSeconds -= Time.deltaTime;
            if (moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                CurrentState = EnemyState.idle;
            }

            if (characterInRange != null)
            {
                directionVector = characterInRange.transform.position - transform.position;
                directionVector.Normalize();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                CurrentState = EnemyState.walking;
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            }
        }

        if(_health <= 0)
        {
            directionVector = Vector2.zero;
        }

        SetDirection(directionVector);
    }

    public void SetCharacterInRange(GameObject characterInRange)
    {
        this.characterInRange = characterInRange;
    }

    public void Attack(GameObject character)
    {
        if (CurrentState != EnemyState.attacking)
        {
            character.GetComponent<Character>().DealDamage(DamagePerHit);
            StartCoroutine(AttackCo());
        }
    }

    IEnumerator AttackCo()
    {
        CurrentState = EnemyState.attacking;
        yield return new WaitForSeconds(1f);
        CurrentState = EnemyState.idle;
    }
}

﻿using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyBase
{
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

    private void FixedUpdate()
    {
        UpdateDirection();
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

    

    public override void OnAttack(GameObject character)
    {
        if (CurrentState != EnemyState.attacking)
        {
            var damage = Calculator.CalculateDamage(MinDamage, MaxDamage);            
            character.GetComponent<Character>().DealDamage(damage, Calculator.IsLastRollCritical);
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
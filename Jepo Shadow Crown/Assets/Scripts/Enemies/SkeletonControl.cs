using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControl : EnemyBase
{
    GameObject _characterInRange;
     
    private float _moveTimeSeconds;
    private float _minMoveTime = 1f;
    private float _maxMoveTime = 1.75f;
    private float _waitTimeSeconds;
    public float _minWaitTime = 0.1f;
    public float _maxWaitTime = 0.75f;

    private void Start()
    {
        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        _waitTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
        ChangeDirection();
    }

    private void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (CurrentState == MovementState.walking)
        {
            _moveTimeSeconds -= Time.deltaTime;
            if (_moveTimeSeconds <= 0)
            {
                _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                CurrentState = MovementState.idle;
            }

            if (_characterInRange != null)
            {
                _directionVector = _characterInRange.transform.position - transform.position;
                _directionVector.Normalize();
            }
        }
        else
        {
            _waitTimeSeconds -= Time.deltaTime;
            if (_waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                CurrentState = MovementState.walking;
                _waitTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
            }
        }

        if(_health <= 0)
        {
            _directionVector = Vector2.zero;
        }

        SetDirection(_directionVector);
    }

    public void SetCharacterInRange(GameObject characterInRange)
    {
        _characterInRange = characterInRange;
    }

    public void OnHitCharacter(GameObject character)
    {
        CurrentState = MovementState.attacking;

        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();

        _characterInRange = null;
        
        character.GetComponent<Character>().Health.DealDamage(DamagePerHit);

        CurrentState = MovementState.idle;
    }
}

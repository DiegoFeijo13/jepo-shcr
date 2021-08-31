using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControll : EnemyBaseControl
{
    public float PushStrength;
    public float PushTime;
    public AttackableEnemy AttackableEnemy;

    GameObject _characterInRange;

    private Vector2 _directionVector;
    private bool _isMoving;
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
        if (_isMoving)
        {
            _moveTimeSeconds -= Time.deltaTime;
            if (_moveTimeSeconds <= 0)
            {
                _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                _isMoving = false;
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
                _isMoving = true;
                _waitTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
            }
        }

        if(AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0)
        {
            _directionVector = Vector2.zero;
        }

        SetDirection(_directionVector);
    }

    void ChangeDirection()
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

    private void ChooseDifferentDirection()
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

    public void SetCharacterInRange(GameObject characterInRange)
    {
        _characterInRange = characterInRange;
    }

    public void OnHitCharacter(GameObject character)
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();

        _characterInRange = null;

        
        character.GetComponent<CharacterMovementModel>().PushCharacter(direction * PushStrength, PushTime);
        character.GetComponent<Character>().Health.DealDamage(AttackableEnemy.DamagePerHit);
    }
}

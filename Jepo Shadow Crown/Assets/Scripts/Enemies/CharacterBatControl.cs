using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBatControl : EnemyBaseControl
{
    public float PushStrength;
    public float PushTime;
    public AttackableEnemy AttackableEnemy;

    GameObject _characterInRange;

    private void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;

        if(_characterInRange != null)
        {
            direction = _characterInRange.transform.position - transform.position;
            direction.Normalize();            
        }

        if(AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0)
        {
            direction = Vector2.zero;
        }

        SetDirection(direction);
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
    }
}

using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyBase
{
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

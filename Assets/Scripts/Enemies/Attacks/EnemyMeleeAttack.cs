using Assets.Scripts.General;
using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttackBase
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            OnAttack(collider.gameObject);
        }
    }

    internal override void OnAttack(GameObject character)
    {
        if (enemyBase.GetCurrentState() != EnemyState.attacking)
        {
            var damage = Calculator.CalculateDamage(MinDamage, MaxDamage);
            character.GetComponent<Character>().DealDamage(damage, Calculator.IsLastRollCritical);
            StartCoroutine(AttackCo());
        }
    }

    IEnumerator AttackCo()
    {
        enemyBase.SetState(EnemyState.attacking);
        yield return new WaitForSeconds(AttackCooldown);
        enemyBase.SetState(EnemyState.idle);
    }
}

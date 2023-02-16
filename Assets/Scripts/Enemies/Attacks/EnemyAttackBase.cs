using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    [SerializeField] protected int MinDamage;
    [SerializeField] protected int MaxDamage;
    [SerializeField] protected float AttackCooldown;

    protected EnemyBase enemyBase;

    protected void Awake()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
    }

    internal abstract void OnAttack(GameObject character);
}

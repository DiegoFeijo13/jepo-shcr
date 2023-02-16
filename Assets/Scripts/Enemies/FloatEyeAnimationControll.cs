using UnityEngine;

public class FloatEyeAnimationControll : MonoBehaviour
{
    [SerializeField] private EnemyRangedAttack rangedAttack;

    internal void Shoot()
    {
        rangedAttack.Shoot();
    }
}

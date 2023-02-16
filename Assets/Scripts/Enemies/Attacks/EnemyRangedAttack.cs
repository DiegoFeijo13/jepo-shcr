using System.Collections;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttackBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce;
    [SerializeField] private Transform spawnPoint;

    private bool canShoot = true;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            OnAttack(collider.gameObject);
        }
    }

    internal override void OnAttack(GameObject character)
    {
        if (!canShoot)
            return;

        player = character;

        enemyBase.SetState(EnemyState.attacking);
        animator.SetBool("IsShooting", true);
    }

    internal void Shoot()
    {
        animator.SetBool("IsShooting", false);
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();

        enemyProjectile.SetDamage(MinDamage, MaxDamage);

        var direction = player.transform.position - spawnPoint.position;
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);

        StartCoroutine(CooldownCo());

        enemyBase.SetState(EnemyState.idle);
    }

    IEnumerator CooldownCo()
    {
        canShoot = false;

        yield return new WaitForSeconds(AttackCooldown);

        canShoot = true;
    }


}

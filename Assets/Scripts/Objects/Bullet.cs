using System.Collections;
using UnityEngine;
using Assets.Scripts.General;

namespace Assets.Scripts.Objects
{
    public class Bullet : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private int minDamage = 30;
        [SerializeField] private int maxDamage = 100;

        [Header("Visual")]
        [SerializeField] private float lifetime;
        [SerializeField] private float knockTime = 0.3f;

        private float lifetimeCounter;

        void Start()
        {
            lifetimeCounter = lifetime;
        }

        void FixedUpdate()
        {
            lifetimeCounter -= Time.deltaTime;
            if (lifetimeCounter <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Walls") || collision.gameObject.CompareTag("Objects"))
            {
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemyBase = collision.gameObject.GetComponent<EnemyBase>();
                if (enemyBase != null)
                {
                    int damage = Calculator.CalculateDamage(minDamage, maxDamage);
                    enemyBase.OnHit(gameObject.transform.position, ItemType.Bullet, damage, Calculator.IsLastRollCritical);
                }

                StartCoroutine(KnockCo(collision.gameObject));

                Destroy(this.gameObject);
            }
        }

        private IEnumerator KnockCo(GameObject obj)
        {
            var body = obj.GetComponent<Rigidbody2D>();

            if (body != null)
            {
                yield return new WaitForSeconds(knockTime);
                body.velocity = Vector2.zero;

            }
        }
    }
}

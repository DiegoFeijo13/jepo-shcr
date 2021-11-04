using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Bullet : MonoBehaviour
    {
        public float damage = 1f;
        public float lifetime;
        public float thrust = 4f;        
        public float knockTime = 0.3f;

        private float lifetimeCounter;

        void Start()
        {
            lifetimeCounter = lifetime;
        }

        void Update()
        {
            lifetimeCounter -= Time.deltaTime;
            if (lifetimeCounter <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemyBase = collision.gameObject.GetComponent<EnemyBase>();                
                if (enemyBase != null)
                {
                    enemyBase.OnHit(gameObject.transform.position, ItemType.Bullet, damage);
                }
                StartCoroutine(KnockCo(collision));
                
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Walls") || collision.gameObject.CompareTag("Objects"))
            {
                Destroy(this.gameObject);
            }
        }

        private void Knock(Collider2D collision)
        {
            
            var body = collision.GetComponent<Rigidbody2D>();
            //BaseMovementModel baseMovementModel = collision.gameObject.GetComponent<BaseMovementModel>();
            if (body != null 
                //&& baseMovementModel != null
                )
            {
                Vector2 difference = body.transform.position - transform.position;
                difference = difference.normalized * thrust;
                body.AddForce(difference, ForceMode2D.Impulse);

                //baseMovementModel.CurrentState = MovementState.staggering;
                StartCoroutine(KnockCo(collision));
            }

            
        }

        private IEnumerator KnockCo(Collider2D collision)
        {
            var body = collision.gameObject.GetComponent<Rigidbody2D>();
            //var baseMovementModel = collision.gameObject.GetComponent<BaseMovementModel>();
            if (body != null)
            {
                yield return new WaitForSeconds(knockTime);
                body.velocity = Vector2.zero;
                //baseMovementModel.CurrentState = MovementState.idle;
            }
        }

    }
}

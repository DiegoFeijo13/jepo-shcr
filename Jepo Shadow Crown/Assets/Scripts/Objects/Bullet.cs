using System;
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

                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Walls"))
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}

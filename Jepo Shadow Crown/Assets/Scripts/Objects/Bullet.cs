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
        public float speed;
        public Rigidbody2D myRigidbody;
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

        public void Setup(Vector2 velocity, Vector3 direction)
        {
            myRigidbody.velocity = velocity.normalized * speed;
            transform.rotation = Quaternion.Euler(direction);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(this.gameObject);
            }

            if (other.gameObject.CompareTag("Walls"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

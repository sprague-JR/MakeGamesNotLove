using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts.boons
{
    public class FIREBALLLZZZZ : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 start;
        private bool isFlying;
        public float speed = 1;
        public float range;
        public uint dmg;

        public void init()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!isFlying) return;
            if (!((rb.position - start).magnitude >= range)) return;
            
            isFlying = false;
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }

        public void attack(Vector2 position, Vector2 direction)
        {
            rb.MovePosition(position);
            rb.velocity = direction.normalized * speed;
            start = position;
            isFlying = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
             if (other.CompareTag("Enemy"))
             {
                 Enemy enemy = other.gameObject.GetComponent<Enemy>();
                 enemy.takeDmg(dmg);
                 Destroy(gameObject);
             }
        }
    }
}
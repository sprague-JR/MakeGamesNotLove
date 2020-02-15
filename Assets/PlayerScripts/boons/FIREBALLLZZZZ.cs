using System;
using UnityEngine;

namespace PlayerScripts.boons
{
    public class FIREBALLLZZZZ : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 start;
        private bool isFlying;
        public float range;

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
            GameObject.Destroy(this.gameObject);
        }

        public void attack(Vector2 position, Vector2 direction)
        {
            rb.MovePosition(position);
            rb.velocity = direction.normalized;
            start = position;
            isFlying = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                
            }
        }
    }
}
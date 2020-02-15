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

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!isFlying) return;
            if (!((rb.position - start).magnitude >= range)) return;
            
            isFlying = false;
            rb.velocity = Vector2.zero;
        }

        public void attack(Vector2 position, Vector2 direction)
        {
            Debug.Log("FFFFIIIIIRRRREEEEE");
            rb.MovePosition(position);
            rb.velocity = direction.normalized;
            start = position;
            isFlying = true;
        }
    }
}
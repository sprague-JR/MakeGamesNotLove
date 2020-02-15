using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {
        public float speed;
        public PlayerController player;
        
        private Vector2 moveVelocity;
        private Rigidbody2D rb;
        private Vector2 pos;
        private const float force = 3f;

        private void FixedUpdate()
        {
            rb.MovePosition(pos);
            var position = rb.position;
            Vector2 move = (player.rb.position - position).normalized;
            moveVelocity = move * speed;
            pos = position + moveVelocity * Time.fixedDeltaTime;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag($"Player"))
            {
                Debug.Log("CD");
                Vector2 dir = other.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
                dir = -dir.normalized;
                Debug.Log(rb.position + dir*force);
                pos = rb.position + dir*force;
            }
        }
    }
}
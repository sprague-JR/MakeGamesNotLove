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

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 move = (player.position - rb.position).normalized;
            moveVelocity = move * speed;
        }
    }
}
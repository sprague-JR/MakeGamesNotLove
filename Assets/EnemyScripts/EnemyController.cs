using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {
        public float speed;
        
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
            Vector2 move = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            moveVelocity = move * speed;
        }
    }
}
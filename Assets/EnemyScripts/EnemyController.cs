using System;
using UnityEngine;
using Random = UnityEngine.Random;
using PlayerScripts;

namespace EnemyScripts
{
    public class EnemyController : MonoBehaviour
    {
        public float speed;
        public float force = 10f;
        
        private PlayerController player;
        private Vector2 moveVelocity;
        private Rigidbody2D rb;
        private Vector2 pos, offset;

        

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }


        private void FixedUpdate()
        {

            var position = rb.position;
            Vector2 move = (player.rb.position - position).normalized;
            moveVelocity = move * speed;
            pos = position + (moveVelocity + offset) * Time.fixedDeltaTime;
            offset = Vector2.zero;
            rb.MovePosition(pos);

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag($"Player"))
            {
                Debug.Log("CD");
                Vector2 dir = other.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
                dir = -dir.normalized;
                offset = dir*force;
            }
        }
    }
}
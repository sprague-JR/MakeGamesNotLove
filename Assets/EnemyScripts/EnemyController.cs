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
            player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        }


        private void FixedUpdate()
        {

			if (Vector3.Magnitude(player.transform.position - transform.position) < 20)
			{
				var position = rb.position;
				Vector2 move = (player.rb.position - position).normalized;
				moveVelocity = move * speed;
				pos = position + (moveVelocity + offset) * Time.fixedDeltaTime;
				offset = Vector2.zero;
				rb.MovePosition(pos);
			}
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"Player"))
            {
                Vector2 dir = other.attachedRigidbody.position - new Vector2(transform.position.x, transform.position.y);
                dir = -dir.normalized;
                offset = dir*force;
            }
        }
    }
}
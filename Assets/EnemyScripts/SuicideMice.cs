using System;
using UnityEngine;
using WeaponScripts;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class SuicideMice : Enemy
    {
        private AoeAttack attack;
        private Rigidbody2D rb;
        private bool detonating;
        private Vector2 start;
        private Vector2 end;
        private float speed;
        private float frac;
        private AudioSource audioSource;

        private void Start()
        {
            attack = GetComponent<AoeAttack>();
            rb = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            start = rb.position;
            end = new Vector2(Random.Range(-4f, 4f) + transform.position.x, Random.Range(-4f, 4f) + transform.position.y);
            speed = 0.7f;
        }

        public override int dmg()
        {
            return 1;
        }

        public override void takeDmg(int dmg)
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (detonating && attack.isDone)
            {
                Destroy(gameObject);
            }

            if (frac >= 1)
            {
                start = rb.position;
                end = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
				end = new Vector2(end.x + transform.position.x,end.y + transform.position.y);
                frac = 0;
            }
            else
            {
                frac += speed * Time.deltaTime;
                rb.MovePosition(Vector2.Lerp(transform.position, end, speed * Time.deltaTime));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                attack.attackPlayer = true;
                attack.attack(rb.position, Vector2.zero);
                detonating = true;
                audioSource.Play();
            }
        }
    }
}
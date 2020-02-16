using System;
using PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class MeleeEnemy : Enemy
    {
        public int health = 10;
        private SpriteRenderer renderer;
        public Sprite coolGuy;

        private void Start()
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
            if (Random.Range(0.0f, 10.0f) >= 9.0f)
            {
                renderer.sprite = coolGuy;
            }
        }

        public override int dmg()
        {
            return 1;
        }

        public override void takeDmg(int dmg)
        {
            Debug.Log(dmg);
            health -= dmg;
            if (health < 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.takeDmg(dmg());
            }
        }
    }
}
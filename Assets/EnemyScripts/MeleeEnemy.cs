using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class MeleeEnemy : Enemy
    {
        public uint health = 10;

        public override uint dmg()
        {
            return 1;
        }

        public override void takeDmg(uint dmg)
        {
            health -= dmg;
            if (health == 0)
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
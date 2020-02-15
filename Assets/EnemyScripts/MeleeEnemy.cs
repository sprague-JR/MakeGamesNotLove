using System;
using UnityEngine;

namespace EnemyScripts
{
    public class MeleeEnemy : Enemy
    {
        public uint health = 10;

        public override uint dmg()
        {
            return 10;
        }

        public override void takeDmg(uint dmg)
        {
            health -= dmg;
            if (health == 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
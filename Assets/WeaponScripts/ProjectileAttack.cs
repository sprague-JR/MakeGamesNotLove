using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;


namespace WeaponScripts
{
    class ProjectileAttack: Boon
    {
        override public float cooldown()
        {
            return 0.5f;
        }

        override public float range()
        {
            return 10;
        }

        override public int damage()
        {
            return 4;
        }

        override public DamageType damageType()
        {
            return DamageType.Projectile;
        }

        override public void attack(Vector2 Position, Vector2 Direction)
        {
            Debug.Log("projectile attack");
        }
    }
}

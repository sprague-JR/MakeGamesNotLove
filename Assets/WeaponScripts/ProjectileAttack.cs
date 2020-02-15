using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;


namespace WeaponScripts
{
    class ProjectileAttack: Boon
    {
        override public uint damage()
        {
            return 0;
        }

        override public DamageType damageType()
        {
            return 0;
        }

        override public void attack(Vector2 Position, Vector2 Direction)
        {
            Debug.Log("projectile attack");
        }
    }
}

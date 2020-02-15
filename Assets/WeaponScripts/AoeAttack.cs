using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace WeaponScripts
{
    class AoeAttack: MonoBehaviour, Boon
    {
        public float cooldown = 0.0f;
        public float range = 10.0f;

        public God god()
        {
            return null;
        }

        public uint damage()
        {
            return 0;
        }

        public DamageType damageType()
        {
            return 0;
        }

        public void attack(Vector2 Position, Vector2 Direction)
        {
            Debug.Log("aoe attack");
        }
    }
}

using UnityEngine;
using WeaponScripts;

namespace PlayerScripts.boons
{
    class RunnyBoon: AoeAttack
    {
        override public float cooldown()
        {
            return 0.6f;
        }

        override public float range()
        {
            return 3.5f;
        }

        override public int damage()
        {
            return 4;
        }
    }
}

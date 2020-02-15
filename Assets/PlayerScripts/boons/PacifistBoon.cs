using UnityEngine;
using WeaponScripts;

namespace PlayerScripts.boons
{
    class PacifistBoon : AoeAttack
    {
        public override float range()
        {
            return 6f;
        }

        public override float cooldown()
        {
            return 1f;
        }

        override public uint damage()
        {
            return 10;
        }
    }
}

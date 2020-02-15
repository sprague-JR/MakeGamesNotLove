using UnityEngine;
using WeaponScripts;

namespace PlayerScripts.boons
{
    class MurderousBoon: BallzMaker
    {
        override public float cooldown()
        {
            return 0.2f;
        }

        override public float range()
        {
            return 15f;
        }

        override public uint damage()
        {
            return 3;
        }
    }
}

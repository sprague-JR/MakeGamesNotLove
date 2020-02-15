﻿using UnityEngine;
using WeaponScripts;

namespace PlayerScripts.boons
{
    class RunnyBoon: MeleeAttack
    {
        override public float cooldown()
        {
            return 1f;
        }

        override public float range()
        {
            return 2f;
        }

        override public uint damage()
        {
            return 10;
        }
    }
}

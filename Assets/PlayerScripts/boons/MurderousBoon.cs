using System;
using UnityEngine;
using WeaponScripts;

namespace PlayerScripts.boons
{
    class MurderousBoon: BallzMaker
    {
        private void Start()
        {
            speed = 15f;
        }

        override public float cooldown()
        {
            return 0.15f;
        }

        override public float range()
        {
            return 15f;
        }

        override public int damage()
        {
            return 3;
        }
    }
}

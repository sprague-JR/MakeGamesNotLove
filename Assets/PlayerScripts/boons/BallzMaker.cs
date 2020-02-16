using UnityEngine;

namespace PlayerScripts.boons
{
    public class BallzMaker : Boon
    {
        public FIREBALLLZZZZ ball;
        public float speed = 8f;

        override public float cooldown()
        {
            return 0.5f;
        }

        override public float range()
        {
            return 13f;
        }

        public override int damage()
        {
            return 6;
        }

        public override DamageType damageType()
        {
            return DamageType.Projectile;
        }

        public override void attack(Vector2 position, Vector2 direction)
        {
            FIREBALLLZZZZ newBall = Instantiate(ball, position, Quaternion.identity);
            newBall.init();
            newBall.range = range();
            newBall.dmg = damage();
            newBall.attack(position, direction, speed);
        }
    }
}
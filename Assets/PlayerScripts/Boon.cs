using UnityEngine;

namespace PlayerScripts
{
    public abstract class Boon : MonoBehaviour
    {
        public float cooldown;
        public float range;

        public abstract uint damage();
        public abstract DamageType damageType();
        public abstract void attack(Vector2 position, Vector2 direction);
    }
}
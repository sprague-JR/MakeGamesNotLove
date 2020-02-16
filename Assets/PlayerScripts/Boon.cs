using UnityEngine;

namespace PlayerScripts
{
    public abstract class Boon : MonoBehaviour
    {
        public abstract float cooldown();
        public abstract float range();
        public abstract int damage();
        public abstract DamageType damageType();
        public abstract void attack(Vector2 position, Vector2 direction);
    }
}
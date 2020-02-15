using UnityEngine;

namespace PlayerScripts
{
    public interface Boon : MonoBehaviour
    {
        God god();
        uint damage();
        DamageType damageType();
        void attack(Vector2 position, Vector2 direction);
    }
}
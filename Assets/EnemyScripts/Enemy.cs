using UnityEngine;

namespace EnemyScripts
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract int dmg();
        public abstract void takeDmg(int dmg);
    }
}
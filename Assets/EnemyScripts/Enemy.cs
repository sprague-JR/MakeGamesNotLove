using UnityEngine;

namespace EnemyScripts
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract uint dmg();
        public abstract void takeDmg(uint dmg);
    }
}
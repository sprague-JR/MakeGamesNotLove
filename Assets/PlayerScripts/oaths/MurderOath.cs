using UnityEngine;

namespace PlayerScripts.oaths
{
    public class MurderOath : MonoBehaviour, Oath
    {

        private bool broke = true;

        public GameObject[] enemies;

        public new string name()
        {
            return "Rip and Tear";
        }

        public bool didBreak()
        {
            return broke;
        }

        public bool hasBeenBroken()
        {
            return broke;
        }

        public void forceBreak(bool b)
        {
            broke = b;
        }

        public void countEnemies()
        {
            if (GameObject.FindGameObjectsWithTag("Enemy") != null)
            {
                // if some enemies remain
                broke = true;

            }
        }
    }
}
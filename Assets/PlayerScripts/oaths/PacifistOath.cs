using UnityEngine;

namespace PlayerScripts.oaths
{
    public class PacifistOath : MonoBehaviour, Oath
    {
        private bool attacked = true;

        public void playerAttack(bool a)
        {
            attacked = a;
        }
        
        public new string name()
        {
            return "No killy Billy";
        }

        public bool didBreak()
        {
            return attacked;
        }

        public bool hasBeenBroken()
        {
            return attacked;
        }

        public void forceBreak(bool b)
        {
            attacked = b;
        }
    }
}
using UnityEngine;

namespace PlayerScripts.oaths
{
    public class PacifistOath : MonoBehaviour, Oath
    {
        public new string name()
        {
            return "No killy Billy";
        }

        public bool didBreak()
        {
            throw new System.NotImplementedException();
        }

        public bool hasBeenBroken()
        {
            throw new System.NotImplementedException();
        }
    }
}
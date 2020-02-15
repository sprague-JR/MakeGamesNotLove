using UnityEngine;

namespace PlayerScripts.oaths
{
    public class MurderOath : MonoBehaviour, Oath
    {
        public new string name()
        {
            return "Rip and Tear";
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
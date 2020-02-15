using UnityEngine;

namespace PlayerScripts.oaths
{
    public class RunnyBoiOath : MonoBehaviour, Oath
    {
        public new string name()
        {
            return "Keep moving and nobody dies";
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
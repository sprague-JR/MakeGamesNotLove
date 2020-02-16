using UnityEngine;

namespace PlayerScripts.oaths
{
    public class RunnyBoiOath : MonoBehaviour, Oath
    {
        private bool broke = true;
        
        public new string name()
        {
            return "Keep moving and nobody dies";
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
            //if(b == false) totems[index].color = Color.grey;
            broke = b;
        }
    }
}
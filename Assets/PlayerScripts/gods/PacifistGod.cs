using UnityEngine;

namespace PlayerScripts.gods
{
    class PacifistGod : MonoBehaviour, God
    {
        Boon pacifistBoon;
        Oath pacifistOath;

        void Start()
        {
            //fireyBoon = ;
            //fireyOath = ;
        }

        public new string name()
        {
            return "PacifistGod";
        }

        public Boon boon()
        {
            return pacifistBoon;
        }

        public Oath oath()
        {
            return pacifistOath;
        }
    }
}

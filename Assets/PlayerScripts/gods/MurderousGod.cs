using UnityEngine;

namespace PlayerScripts.gods
{
    class MurderousGod : MonoBehaviour, God
    {
        Boon murderousBoon;
        Oath murderousOath;

        void Start()
        {
            //fireyBoon = ;
            //fireyOath = ;
        }

        public new string name()
        {
            return "MurderousGod";
        }

        public Boon boon()
        {
            return murderousBoon;
        }

        public Oath oath()
        {
            return murderousOath;
        }
    }
}

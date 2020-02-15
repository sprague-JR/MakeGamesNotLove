using UnityEngine;

namespace PlayerScripts.gods
{
    class MurderousGod : MonoBehaviour, God
    {
        Boon murderousBoon;
        Oath murderousOath;

        void Start()
        {
            murderousBoon = GetComponentInChildren<Boon>();
            murderousOath = GetComponentInChildren<Oath>();
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

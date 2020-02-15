using UnityEngine;


namespace PlayerScripts.gods
{
    class RunnyGod : MonoBehaviour, God
    {
        Boon runnyBoon;
        Oath runnyOath;

        void Start()
        {
            //fireyBoon = ;
            //fireyOath = ;
        }

        public new string name()
        {
            return "RunnyGod";
        }

        public Boon boon()
        {
            return runnyBoon;
        }

        public Oath oath()
        {
            return runnyOath;
        }
    }
}

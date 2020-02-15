using UnityEngine;


namespace PlayerScripts.gods
{
    class RunnyGod : MonoBehaviour, God
    {
        Boon runnyBoon;
        Oath runnyOath;

        void Start()
        {
            runnyBoon = gameObject.GetComponentInChildren<Boon>();
            runnyOath = gameObject.GetComponentInChildren<Oath>();
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

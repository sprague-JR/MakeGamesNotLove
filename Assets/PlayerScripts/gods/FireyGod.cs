using UnityEngine;
using PlayerScripts.oaths;
using PlayerScripts.boons;

namespace PlayerScripts.gods
{
    public class FireyGod : MonoBehaviour, God
    {
        Boon fireyBoon;
        Oath fireyOath;

        void Start()
        {
            fireyBoon = GetComponentInChildren<BallzMaker>();
            fireyOath = GetComponentInChildren<FyreOath>();
        }

        public new string name()
        {
            return "BallllzMaker";
        }

        public Boon boon()
        {
            return fireyBoon;
        }

        public Oath oath()
        {
            return fireyOath;
        }
    }
}
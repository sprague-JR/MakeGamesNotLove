using System;
using UnityEngine;

namespace PlayerScripts.gods
{
    public class PacifistGod : MonoBehaviour, God
    {
        private Boon pacifistBoon;
        private Oath pacifistOath;

        private void Start()
        {
            
        }

        public string name()
        {
            return "Anti-Gandhi";
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
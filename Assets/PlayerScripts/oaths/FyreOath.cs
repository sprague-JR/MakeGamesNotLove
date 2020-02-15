using System;
using PlayerScripts.gods;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlayerScripts.oaths
{
    public class FyreOath : MonoBehaviour, Oath
    {
        private bool broke;
        private God fireyGod;

        private void Start()
        {
            fireyGod = new FireyGod();
        }

        public new string name()
        {
            return "Fyre Boi";
        }

        public bool didBreak()
        {
            return broke;
        }

        public bool hasBeenBroken()
        {
            return broke;
        }

        public God god()
        {
            return fireyGod;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                broke = true;
            }
        }
    }
}
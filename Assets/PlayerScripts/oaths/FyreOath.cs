using System;
using PlayerScripts.gods;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlayerScripts.oaths
{
    public class FyreOath : MonoBehaviour, Oath
    {
        private bool broke = true;

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

        public void forceBreak(bool b)
        {
            broke = b;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.CompareTag("Water"))
            {
                Debug.Log("Splish Splosh");
                broke = true;
            }
        }
        
        
    }
}
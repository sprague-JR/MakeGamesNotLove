using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public uint health;
        public PlayerController controller;
        
        private Boon boon;

        private void Start()
        {
            // throw new NotImplementedException();
        }

        private void Update()
        {
            // throw new NotImplementedException();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag($"Enemy"))
            {
                health -= 10;
                if (health == 0)
                {
                    Debug.Log("Ya dead ya cunt");
                }
            }
        }
    }
}
using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public int health;
        public PlayerController controller;
        public bool isDead=false;
        
        private Boon boon;

        public void itsaDeadBoi()
        {
            //run animation
            isDead = true; 
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag($"Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                health -= (int)enemy.dmg();
                if (health == 0)
                {
                    itsaDeadBoi();
                    Debug.Log("Ya dead ya cunt");
                }
            }
        }
    }
}
using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public uint health;
        public PlayerController controller;
        
        private Boon boon;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag($"Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                health -= enemy.dmg();
                if (health == 0)
                {
                    Debug.Log("Ya dead ya cunt");
                }
            }
        }
    }
}
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
        public bool nextLevel = false;
        
        private Boon boon;

        public void itsaDeadBoi()
        {
            //run animation
            isDead = true; 
        }

        public void takeDmg(int dmg)
        {
            health -= (int) dmg;
            if (health <= 0)
            {
                itsaDeadBoi();
                Debug.Log("Ya dead ya cunt");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            controller.interactTag = other.tag;
            if(other.tag == "Exit")
            {
                nextLevel = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.Contains("Totem"))
            {
                controller.canInteract = false;
            } 

        }
    }
}
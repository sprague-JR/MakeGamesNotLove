using System;
using EnemyScripts;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public int health;
        public PlayerController controller;

        [HideInInspector]
        public bool isDead=false;
        [HideInInspector]
        public bool nextLevel = false;

        public Sprite fullHeart;
        public Sprite emptyHeart;

        private Image[] hearts;
        private const int fullHealth = 5;

        private Boon boon;

        private void Start()
        {
            hearts = new Image[fullHealth];
            hearts = GameObject.Find("Hearts").GetComponentsInChildren<Image>();
           
        }

        private void Update()
        {
            for (int i = 0; i < fullHealth; i++)
            {
                if(i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }

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
                health = 0;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;
using PlayerScripts.gods;
using PlayerScripts.oaths;

namespace PlayerScripts 
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;

        private Vector2 moveInput;
        private Vector2 direction;
        private Vector2 moveVelocity;

        private Boon boon;
        private MeleeAttack meleeAt;
        private GodManager godManager;
        private Oath runnyOath;
        [HideInInspector]
        public Rigidbody2D rb;

        void Start()
        {
            direction = Vector2.up;
            rb = GetComponent<Rigidbody2D>();
            meleeAt = GetComponent<MeleeAttack>();
            godManager = GetComponent<GodManager>();
            godManager.init();
            runnyOath = GameObject.Find("RunnyGod").GetComponentInChildren<Oath>();
        }

        // Update is called once per frame
        void Update()
        {
            // get horizontal and vertical inputs and calculate the corresponding speed
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput * speed;
            if (moveInput != Vector2.zero)
            {
                direction = moveInput;
            }
            else
            {
                if (!runnyOath.hasBeenBroken())
                {
                    runnyOath.forceBreak(true);
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                godManager.attack(3, transform.position, direction);
            }
            else if(Input.GetButtonDown("Fire2"))
            {
                godManager.attack(1, transform.position, direction);
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                godManager.attack(2, transform.position, direction);
            }
            else if (Input.GetButtonDown("Fire4"))
            {
                godManager.attack(0, transform.position, direction);
            }
        }

        private void FixedUpdate()
        {
            movePosition();
        }


        private void movePosition()
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
}

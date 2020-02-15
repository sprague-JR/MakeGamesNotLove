using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace PlayerScripts 
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;

        private Vector2 moveInput;
        private Vector2 direction;
        private Vector2 moveVelocity;

        private MeleeAttack meleeAt;
        private BoonManager boonManager;
        [HideInInspector]
        public Rigidbody2D rb;

        void Start()
        {
            direction = Vector2.up;
            rb = GetComponent<Rigidbody2D>();
            meleeAt = GetComponent<MeleeAttack>();
            boonManager = GetComponent<BoonManager>();
            boonManager.init();
            for (int i = 0; i < 4; i++)
            {
                boonManager.enableBoon(i);
            }
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

            if (Input.GetButtonDown("Fire1"))
            {
                boonManager.attack(3, transform.position, direction);
            }
            else if(Input.GetButtonDown("Fire2"))
            {
                boonManager.attack(1, transform.position, direction);
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                boonManager.attack(2, transform.position, direction);
            }
            else if (Input.GetButtonDown("Fire4"))
            {
                boonManager.attack(0, transform.position, direction);
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

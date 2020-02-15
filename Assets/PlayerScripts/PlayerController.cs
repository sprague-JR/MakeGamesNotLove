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
        private Vector2 moveVelocity;

        private Boon boon;
        private MeleeAttack meleeAt;
        private BoonManager boonManager;
        [HideInInspector]
        public Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            meleeAt = GetComponent<MeleeAttack>();
            boonManager = GetComponent<BoonManager>();
        }

        // Update is called once per frame
        void Update()
        {
            // get horizontal and vertical inputs and calculate the corresponding speed
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput * speed;

        

            if (Input.GetButtonDown("Fire1"))
            {
                boonManager.boon[0].attack(transform.position, moveInput);
            }
            else if(Input.GetButtonDown("Fire2"))
            {
                boonManager.boon[1].attack(transform.position, moveInput);
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                boonManager.boon[2].attack(transform.position, moveInput);
            }
            else if (Input.GetButtonDown("Fire4"))
            {
                boonManager.boon[3].attack(transform.position, moveInput);
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

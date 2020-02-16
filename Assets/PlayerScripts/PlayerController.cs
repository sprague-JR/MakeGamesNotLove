using System;
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
        public bool canInteract;
        public string interactTag;

        private Vector2 moveInput;
        private Vector2 direction;
        private Vector2 currentAim;
        private Vector2 moveVelocity;

        private Boon boon;
        private MeleeAttack meleeAt;
        private GodManager godManager;
        private Oath runnyOath;
        private bool hasMoved;
        private AudioSource audioSource;

		private Animator myAnimator;

        [HideInInspector]
        public Rigidbody2D rb;

        private static readonly int y = Animator.StringToHash("Y");
        private static readonly int x = Animator.StringToHash("X");

        void Start()
        {
            direction = Vector2.up;
            rb = GetComponent<Rigidbody2D>();
            meleeAt = GetComponent<MeleeAttack>();
            godManager = GetComponent<GodManager>();
            audioSource = GetComponent<AudioSource>();
            godManager.init();
            runnyOath = GameObject.Find("RunnyGod").GetComponentInChildren<Oath>();
			myAnimator = GetComponentInChildren<Animator>();
            moveInput = Vector2.zero;
        }

        // Update is called once per frame
        void Update()
        {
            // get horizontal and vertical inputs and calculate the corresponding speed
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            currentAim.x = Input.GetAxis("AimHoriz");
            currentAim.y = Input.GetAxis("AimVirt");
            moveVelocity = moveInput * speed;

			myAnimator.SetFloat(y, Input.GetAxisRaw("Vertical"));
			myAnimator.SetFloat(x, Input.GetAxisRaw("Horizontal"));

            if (moveInput != Vector2.zero)
            {
                direction = currentAim.normalized;
                hasMoved = true;
            }
            else
            {
                if (hasMoved && !runnyOath.hasBeenBroken())
                {
                    runnyOath.forceBreak(true);
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Fire1")) == 1f)
            {
                godManager.attack(3, transform.position, direction);
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Fire2")) == 1f)
            {
                godManager.attack(1, transform.position, direction);
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Fire3")) == 1f)
            {
                godManager.attack(2, transform.position, direction);
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Fire4")) == 1f)
            {
                godManager.attack(0, transform.position, direction);
            }

            //interact with totems to enable their boons 
            if (Input.GetButtonDown("Interact"))
            {
                switch (interactTag)
                {
                    case "FieryTotem":
                        Debug.Log("FieryToteeeeem");
                        godManager.enableGods(0);
                        audioSource.Play();
                        break;
                    case "PacifistTotem":
                        Debug.Log("pacifist");
                        godManager.notPacifist = false;
                        godManager.enableGods(1);
                        audioSource.Play();
                        break;
                    case "RunnyTotem":
                        Debug.Log("runny");
                        hasMoved = false;
                        godManager.enableGods(2);
                        audioSource.Play();
                        break;
                    case "MurderTotem":
                        Debug.Log("murder");
                        godManager.enableGods(3);
                        audioSource.Play();
                        break;
                    default:
                        break;
                }
            }
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
}

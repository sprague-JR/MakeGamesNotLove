using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Boon boon;
    private MeleeAttack meleeAt;
    [HideInInspector]
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeAt = GetComponent<MeleeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal and vertical inputs and calculate the corresponding speed
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        if (Input.GetButtonDown("Fire1"))
        {
            meleeAt.attack(transform.position, moveInput);
        
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

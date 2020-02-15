using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    [HideInInspector]
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal and vertical inputs and calculate the corresponding speed
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;
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

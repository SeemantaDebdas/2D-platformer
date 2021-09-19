using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float distanceToCheckGrounded;
    [SerializeField] LayerMask groundLayer;
    float movement;
    Rigidbody2D rb;
    [SerializeField] bool isGrounded;
    [SerializeField] bool pressedJump;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialisation();
    }

    void Initialisation()
    {
        pressedJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheckGrounded, groundLayer))
        {
            isGrounded = true;
        }

        Debug.DrawRay(transform.position, Vector3.down * distanceToCheckGrounded);
    }

    private void TakeInput()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.F))
            pressedJump = true;
        else
            pressedJump = false;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (isGrounded && pressedJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(movement * moveForce, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanceToCheckGrounded);
    }
}

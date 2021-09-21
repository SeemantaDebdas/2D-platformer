using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float linearDrag = 0.5f;
    [SerializeField] float distanceToCheckGrounded;
    [SerializeField] LayerMask groundLayer;
    float movement;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] bool isGrounded;
    [SerializeField] bool pressedJump;
    [SerializeField] bool isFacingRight;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
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
        DebugOperations();
    }

    private void DebugOperations()
    {
        Debug.Log(movement);
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
        HandleMovement();
        HandleAnimation();
        //Jump();
    }

    private void HandleAnimation()
    {
        anim.SetFloat("MoveFloat", Mathf.Abs(rb.velocity.x));
    }

    private void Jump()
    {
        if (isGrounded && pressedJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void HandleMovement()
    {
        Move();
        ModifyLinearDrag();
    }

    private void Move()
    {
        //manipulating movement
        rb.AddForce(new Vector2(movement * moveForce, 0), ForceMode2D.Impulse);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2((Mathf.Sign(rb.velocity.x)) * maxSpeed, rb.velocity.y);
        }

        //flipping
        if((movement == 1 && !isFacingRight)||(movement == -1 && isFacingRight))
        {
            Flip();
        }
    }

    private void ModifyLinearDrag()
    {
        if (Mathf.Abs(movement) < 0.1f)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight? 0: 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanceToCheckGrounded);
    }
}

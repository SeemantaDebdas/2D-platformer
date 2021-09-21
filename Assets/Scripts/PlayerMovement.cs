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
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        DebugOperations();
        IsGrounded();
    }

    private void DebugOperations()
    {
        Debug.Log(movement);
    }

    void IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheckGrounded, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void TakeInput()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            pressedJump = true;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
        HandleJump();
    }

    private void HandleAnimation()
    {
        anim.SetFloat("MoveFloat", Mathf.Abs(rb.velocity.x));
    }

    private void HandleJump()
    {
        if (pressedJump)
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            pressedJump = false;
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
        if (isGrounded)
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
        else
        {
            rb.drag = linearDrag * 0.15f;
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

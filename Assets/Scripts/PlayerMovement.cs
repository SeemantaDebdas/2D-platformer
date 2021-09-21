using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float linearDrag = 0.5f;
    [SerializeField] float dragMultiplier = 0.15f;
    float movement;
    Player player;
    PlayerJump playerJump;
    [SerializeField] bool isFacingRight;
    private void Awake()
    {
    }

    private void Start()
    {
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
    }


    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        player.anim.SetFloat("MoveFloat", Mathf.Abs(player.rb.velocity.x));
    }

    private void HandleMovement()
    {
        Move();
        ModifyLinearDrag();
    }

    private void Move()
    {
        //manipulating movement
        player.rb.AddForce(new Vector2(movement * moveForce, 0), ForceMode2D.Impulse);
        if (Mathf.Abs(player.rb.velocity.x) > maxSpeed)
        {
            player.rb.velocity = new Vector2((Mathf.Sign(player.rb.velocity.x)) * maxSpeed, player.rb.velocity.y);
        }

        //flipping
        if((movement == 1 && !isFacingRight)||(movement == -1 && isFacingRight))
        {
            Flip();
        }
    }

    private void ModifyLinearDrag()
    {
        bool changingDirections = (movement > 0 && player.rb.velocity.x < 0
                                || movement < 0 && player.rb.velocity.x > 0);
        if (player.isGrounded)
        {
            if ((Mathf.Abs(movement) < 0.1f || changingDirections) && !playerJump.pressedJump)
            {
                player.rb.drag = linearDrag;
            }
            else
            {
                player.rb.drag = 0;
            }
        }
        else
        {
            player.rb.drag = linearDrag * dragMultiplier;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight? 0: 180, 0);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{ 
    [Header("State Variables")]
    [SerializeField] bool holdingJump;
    public bool pressedJump;

    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float fallMultiplier = 5f;
    [SerializeField] float fallDivider = 2.5f;

    [Header("Cayote Time")]
    [SerializeField] float cayoteTime = 0.2f;
    [SerializeField] float cayoteTimeCounter;

    [Header("Buffer Time")]
    [SerializeField] float bufferTime = 0.1f;
    [SerializeField] float bufferTimeCounter;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        ModifyGravity();
        HandleAnimation();

        HandleCayoteTime();
        HandleBufferTime();
    }

    private void TakeInput()
    {
        
        if (bufferTimeCounter>0 && cayoteTimeCounter > 0)
        {
            pressedJump = true;
        }

        if (Input.GetKey(KeyCode.F))
            holdingJump = true;
        else
            holdingJump = false;
    }

    private void HandleBufferTime()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bufferTimeCounter = bufferTime;
        }
        else
        {
            bufferTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (pressedJump)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
            player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pressedJump = false;

            bufferTimeCounter = 0;
            cayoteTimeCounter = 0;
        }
    }

    void ModifyGravity()
    {
        if (player.isGrounded)
        {
            player.rb.gravityScale = 0;
        }
        else
        {
            player.rb.gravityScale = gravityScale;
            if (player.rb.velocity.y < 0)
            {
                player.rb.gravityScale = gravityScale * fallMultiplier;
            }
            else if (player.rb.velocity.y > 0 && !holdingJump)
            {
                player.rb.gravityScale = gravityScale * (fallMultiplier / fallDivider);
            }
        }
    }

    void HandleCayoteTime()
    {
        //if (Input.GetKeyUp(KeyCode.F))
        //    cayoteTimeCounter = 0f;

        if (player.isGrounded)
            cayoteTimeCounter = cayoteTime;
        else
            cayoteTimeCounter -= Time.deltaTime;
    }

    void HandleAnimation()
    {
        player.anim.SetFloat("JumpFloat", player.rb.velocity.y);
        player.anim.SetBool("FallBool", player.isGrounded);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiedMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] float moveSpeed;
    [Range(0,1)][SerializeField] float smoothDampTime = 0.1f;
    float movement;
    Vector2 currentVelocity;

    [Header("State Variables")]
    [SerializeField] bool isFacingRight;

    Player player;

    private void Awake()
    {
    }

    private void Start()
    {
        player = GetComponent<Player>();
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
        //ModifyLinearDrag();
    }

    private void Move()
    {
        //manipulating movement
        Vector2 targetVelocity = new Vector2(movement * moveSpeed, player.rb.velocity.y);

        player.rb.velocity = Vector2.SmoothDamp(player.rb.velocity, targetVelocity, ref currentVelocity, smoothDampTime);

        //flipping
        if ((movement == 1 && !isFacingRight) || (movement == -1 && isFacingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
    }
}

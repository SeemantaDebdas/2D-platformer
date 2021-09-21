using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask groundLayer;

    public bool isGrounded;
    public float distanceToCheckGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        DebugTexts();
    }

    void DebugTexts()
    {
        Debug.Log("Y velocity: " + Mathf.Sign(rb.velocity.y));
    }

    void IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheckGrounded, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanceToCheckGrounded);
    }
}

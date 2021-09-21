using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    public bool isGrounded;
    public float distanceToCheckGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        DebugTexts();
    }

    void DebugTexts()
    {
        Debug.Log("X velocity: " + rb.velocity.x);
        Debug.Log("Y velocity: " + rb.velocity.y);
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

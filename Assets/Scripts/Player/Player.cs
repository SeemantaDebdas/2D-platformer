using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    public HealthBar healthBar;

    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public bool isGrounded;
    public float distanceToCheckGrounded;

    [Header("General Factors")]
    public int maxHealth = 4;
    public int currentHealth;
    public float playerSpeed = 5f;
    float tempSpeed;

    [Header("State Variables")]
    public bool isCrouched;

    [Header("Particle System")]
    public ParticleSystem footDustTrail;
    public ParticleSystem landingDustParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        isCrouched = false;
        tempSpeed = playerSpeed;
    }

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

        if (isCrouched)
        {
            playerSpeed = 0;
        }
        else
        {
            playerSpeed = tempSpeed;
        }

        //DebugTexts();
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

    public void Damage()
    {
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
        anim.Play("Hit");
    }
}

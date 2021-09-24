using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGhoul : MonoBehaviour,IDamagable
{
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundCheckLayers;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool groundDetected;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float destroyTimer;

    float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics2D.Raycast(groundCheckTransform.position, Vector2.down,groundCheckDistance, groundCheckLayers))
        {
            groundDetected = false;
            Flip();
        }
        else
        {
            groundDetected = true;
        }
        speed = (isFacingRight) ? moveSpeed : -moveSpeed;
        rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(0, (isFacingRight) ? 180 : 0, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(groundCheckTransform.position, Vector3.down * groundCheckDistance);
    }

    public void Damage()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GameObject explosionPrefabSpawn = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionPrefabSpawn, destroyTimer);
        Destroy(this.gameObject, destroyTimer);
    }
}

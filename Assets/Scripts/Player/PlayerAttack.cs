using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] int attackCounter = 0;
    [SerializeField] float attackTimerCounter;
    [SerializeField] float attackTimer;
    [SerializeField] int numberOfAttacks;
    [SerializeField] bool isAttacking;

    [Header("Hitbox Parameters")]
    [SerializeField] float hitBoxTimer;
    [SerializeField] float hitBoxTimerCounter;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        attackCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimerCounter -= Time.deltaTime;
        GroundedAttack();
        FlyingAttack();

        if (attackTimerCounter < 0)
        {
            isAttacking = false;
            attackCounter = 1;
        }

        player.anim.SetFloat("AttackFloat", attackCounter);
        player.anim.SetFloat("AttackTimerFloat", attackTimerCounter);
    }

    private void FlyingAttack()
    {
        if(Input.GetKeyDown(KeyCode.R) && !player.isGrounded)
        {
            player.anim.Play("FlyingKick");
        }
    }

    private void GroundedAttack()
    {
        if (player.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.R) && player.isCrouched)
            {
                player.anim.Play("CrouchedKick");
            }
            else if (!player.isCrouched)
            {
                if (Input.GetKeyDown(KeyCode.R) && !isAttacking)
                {
                    isAttacking = true;
                    player.anim.Play("Punch");
                    attackTimerCounter = attackTimer;
                }
                else if (Input.GetKeyDown(KeyCode.R) && isAttacking && attackTimerCounter > 0)
                {
                    attackCounter++;
                }
            }
        }
    }

    void HitboxActivate()
    {

    }
}

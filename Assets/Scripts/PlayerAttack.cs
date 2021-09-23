using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int attackCounter = 0;
    [SerializeField] float attackTimerCounter;
    [SerializeField] float attackTimer;
    [SerializeField] int numberOfAttacks;
    [SerializeField] bool isAttacking;

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
        if(Input.GetKeyDown(KeyCode.R) && !isAttacking && player.isGrounded)
        {
            isAttacking = true;
            player.anim.Play("Punch");
            attackTimerCounter = attackTimer;
        }
        else if(Input.GetKeyDown(KeyCode.R) && isAttacking && attackTimerCounter > 0)
        {
            attackCounter++;
            //if(attackCounter == numberOfAttacks)
            //{
                
            //}
        }

        player.anim.SetFloat("AttackFloat", attackCounter);
        //player.anim.SetBool("AttackBool", isAttacking);

        if (attackTimerCounter < 0)
        { 
            isAttacking = false;
            attackCounter = 1;
        }

        
        Debug.Log(attackTimerCounter);
    }
}

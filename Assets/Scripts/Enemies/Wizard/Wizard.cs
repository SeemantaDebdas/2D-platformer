using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] bool canAttack;
    [SerializeField] float coolDownTimer = 0.5f;
    [SerializeField] float coolDownTimerCounter;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        coolDownTimerCounter = coolDownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimerCounter -= Time.deltaTime;
        if (coolDownTimerCounter < 0)
        {
            canAttack = true;
            coolDownTimerCounter = coolDownTimer;
        }
        else
        {
            canAttack = false;
        }
        anim.SetBool("AttackBool", canAttack);
    }

}

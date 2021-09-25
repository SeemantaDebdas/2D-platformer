using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamagable
{
    [SerializeField] bool canAttack;
    [SerializeField] bool isHit;
    [SerializeField] float coolDownTimer = 0.5f;
    [SerializeField] float coolDownTimerCounter;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float destroyTimer;

    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        isHit = false;
        anim = GetComponentInChildren<Animator>();
        coolDownTimerCounter = coolDownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
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

    public void Damage()
    {
        isHit = true;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GameObject explosionPrefabSpawn = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionPrefabSpawn, destroyTimer);
        Destroy(this.gameObject, destroyTimer);
    }
}

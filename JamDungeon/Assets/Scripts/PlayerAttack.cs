using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Collider2D attackBox;

    private bool isAttacking = false;
    private bool canAttack = true;
    private float attackTimer = 0f;
    public float attackTime;
    private float attackCooldownTimer = 0f;
    public float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        attackBox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // lmb
        {
            if (canAttack)
            {
                StartAttack();
            }
        }

        if (isAttacking)
        {
            DoAttack();
        }

        if (!canAttack)
        {
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer > attackCooldown)
            {
                canAttack = true;
                attackCooldownTimer = 0f;
            }
        }
    }

    private void StartAttack()
    {
        attackBox.enabled = true;
        isAttacking = true;
        canAttack = false;
    }

    private void DoAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime)
        {
            isAttacking = false;
            attackBox.enabled = false;
            attackTimer = 0;
        }
    }
}

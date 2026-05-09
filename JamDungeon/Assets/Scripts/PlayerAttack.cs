using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    private Collider2D attackBox;
    public GameObject attackVisual;

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
        attackVisual.SetActive(true);

        visualStart = Quaternion.Euler(0, 0, 60f) * transform.parent.up;
        visualEnd = Quaternion.Euler(0, 0, -60f) * transform.parent.up;
    }

    Vector3 visualStart = new Vector3(1f, 0.1f);
    Vector3 visualEnd = new Vector3(-1f, 0.1f);
    private void DoAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime)
        {
            isAttacking = false;
            attackBox.enabled = false;
            attackTimer = 0;
            attackVisual.SetActive(false);
        }

        
        Vector3 attackVisualDir = Vector3.Lerp(visualStart, visualEnd, attackTimer / attackTime);
        attackVisual.transform.up = attackVisualDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("EnemyHurt"))
        {
            return;
        }

        collision.GetComponent<EnemyHealth>().TakeDamage();
    }
}

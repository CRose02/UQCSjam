using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bullet;

    public float bulletForce;
    public float attackCooldown;
    private float attackTimer = 0f;
    public float sightDistance;

    private Transform playerTrans;

    public LayerMask lineOfSight;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        attackTimer += Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerNear = Vector2.Distance(transform.position, playerTrans.position) < sightDistance;

        if (playerNear)
        {
            transform.up = playerTrans.position - transform.position;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, playerTrans.position - transform.position, sightDistance, lineOfSight);

            if (!rayHit)
            {
                attackTimer = 0f;
                return;
            }

            if (!rayHit.transform.CompareTag("Player"))
            {
                attackTimer = 0f;
                return;
            }
            
            //Debug.Log(rayHit.transform.name);

            Shoot();
            attackTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletInst.GetComponent<Rigidbody2D>();
        // Direction to player
        Vector3 directionToPlayer = playerTrans.position - transform.position;
        directionToPlayer.Normalize();

        bulletRb.AddForce(directionToPlayer * bulletForce, ForceMode2D.Impulse);
    }
}

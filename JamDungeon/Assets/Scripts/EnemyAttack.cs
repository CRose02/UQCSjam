using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bullet;

    public float bulletForce;
    public float attackCooldown;
    private float attackTimer = 0f;

    private Transform playerTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if line of sight
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] attackPoints;

    public float bulletForce;
    public float attackCooldown;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {

            Shoot();
            attackTimer = 0f;
        }
    }

    private void Shoot()
    {
        for (int i=0; i< attackPoints.Length; i++)
        {
            GameObject bulletInst = Instantiate(bullet, attackPoints[i].position, Quaternion.identity);
            Rigidbody2D bulletRb = bulletInst.GetComponent<Rigidbody2D>();
            // Direction
            Vector3 dir = attackPoints[i].position - transform.position;
            dir.Normalize();

            bulletRb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
        }
    }
}

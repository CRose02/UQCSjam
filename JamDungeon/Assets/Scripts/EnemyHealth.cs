using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    public GameObject deathparticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        enemyHealth--;

        if (enemyHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathparticles, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}

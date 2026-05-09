using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    public GameObject deathparticles;
    public GameObject hurtParticles;
    public GameObject key;

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
        } else
        {
            Instantiate(hurtParticles, transform.position, Quaternion.identity);
        }
    }

    private void Die()
    {
        Instantiate(deathparticles, transform.position, Quaternion.identity);

        if (GameManager.Instance.ShouldDropKey())
        {
            Instantiate(key, transform.position, Quaternion.identity);
            // get rb off key
            // Apply random dir force impulse
        }

        Destroy(transform.parent.gameObject);
    }
}

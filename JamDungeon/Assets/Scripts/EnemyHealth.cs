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
    public float keyForce;

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
            GameObject keyInst = Instantiate(key, transform.position, Quaternion.identity);
            Rigidbody2D keyRb = keyInst.GetComponent<Rigidbody2D>();
            Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            dir.Normalize();
            keyRb.AddForce(dir * keyForce, ForceMode2D.Impulse);
            // get rb off key
            // Apply random dir force impulse
        }

        Destroy(transform.parent.gameObject);
    }
}

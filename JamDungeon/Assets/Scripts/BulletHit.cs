using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockbackForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerHurtbox"))
        {
            return;
        }

        //BeginInvinsible();
        //BeginFreeze();
        //StartCoroutine(Freeze());
        GameManager.Instance.TakeDamage();

        // instantiate particle effect
        // knockback player
        PlayerDamage playerDamage = collision.gameObject.GetComponentInChildren<PlayerDamage>();
        playerDamage.TakeDamage();
        Rigidbody2D playerRb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        Vector2 dir = rb.velocity;
        dir.Normalize();
        playerRb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("DamagePlayer"))
        {
            return;
        }

        GameManager.Instance.TakeDamage();

        Destroy(collision.gameObject.transform.parent.gameObject);
    }
}

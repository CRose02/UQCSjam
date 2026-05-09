using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;

public class KeyMove : MonoBehaviour
{
    public bool isMoving;
    public float speed;

    private Transform playerTrans;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            return;
        }

        Vector3 dir = (playerTrans.position - transform.position).normalized;
        rb.AddForce(dir * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
}

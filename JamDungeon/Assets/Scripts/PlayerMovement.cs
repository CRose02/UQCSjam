using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float dashForce;
    public float dashCooldown;
    public bool onDashCooldown = false;
    private float dashTimer = 0f;

    private Rigidbody2D rb;
    private float moveForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleDash();
    }

    Vector3 move;
    private void HandleInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        move = new Vector3(x, y, 0f).normalized;
        moveForce = Mathf.Clamp(maxSpeed - rb.velocity.magnitude, 0f, 100f);

        if (move != Vector3.zero)
        {
            transform.up = move;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(move * moveForce);
    }

    private void HandleDash()
    {
        if (onDashCooldown)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer > dashCooldown)
            {
                dashTimer = 0f;
                onDashCooldown = false;
            }

            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space) || move == Vector3.zero)
        {
            return;
        }

        StartDash();

        
    }

    public void StartDash()
    {
        onDashCooldown = true;

        rb.velocity = move * dashForce;
    }
}

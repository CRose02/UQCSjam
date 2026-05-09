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
    private PlayerSquash playerSquash;
    private float moveForce;

    public RectTransform dashRect;
    public float rectStart;
    public float rectSmall;
    public AnimationCurve dashUICurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSquash = GetComponentInChildren<PlayerSquash>();

        rectStart = dashRect.rect.width;
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
            dashRect.sizeDelta = new Vector2(rectStart * dashUICurve.Evaluate(dashTimer/dashCooldown), 22f);
            if (dashTimer > dashCooldown)
            {
                dashTimer = 0f;
                onDashCooldown = false;
                dashRect.sizeDelta = new Vector2(rectStart, 22f);
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
        playerSquash.Squash();

        dashRect.sizeDelta = new Vector2(rectSmall, 22f);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public Collider2D hurtbox;
    public SpriteRenderer spriteRenderer;

    private bool isInvinsible = false;
    public float invinsibleCooldown;
    private float invinsibleTimer = 0f;
    public float flashFrequency;
    public Color flashColor;
    private Color normalColor;
    public AnimationCurve flashColorCurve;

    public float freezeFrameTime;
    public bool isFreeze = false;

    // Start is called before the first frame update
    void Start()
    {
        normalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvinsible)
        {
            DoInvinsible();
            DoFlashing();
        }
    }

    private void BeginInvinsible()
    {
        isInvinsible = true;
        hurtbox.enabled = false;
    }

    private void DoInvinsible()
    {
        invinsibleTimer += Time.deltaTime;
        if (invinsibleTimer > invinsibleCooldown)
        {
            isInvinsible = false;
            invinsibleTimer = 0f;
            hurtbox.enabled = true;
        }

        /*
        if (invinsibleTimer > freezeFrameTime && isFreeze)
        {
            isFreeze = false;
            Time.timeScale = 1f;
        }
        */
    }

    private void DoFlashing()
    {
        spriteRenderer.color = Color.Lerp(normalColor, flashColor, flashColorCurve.Evaluate(invinsibleTimer / flashFrequency));
    }

    private void BeginFreeze()
    {
        isFreeze = true;
        Time.timeScale = 0f;
    }

    public void TakeDamage()
    {
        BeginInvinsible();
        //BeginFreeze();
        StartCoroutine(Freeze());
        GameManager.Instance.TakeDamage();

        //Destroy(collision.gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("DamagePlayer"))
        {
            return;
        }

        BeginInvinsible();
        //BeginFreeze();
        StartCoroutine(Freeze());
        GameManager.Instance.TakeDamage();

        Destroy(collision.gameObject.transform.parent.gameObject);
    }


    IEnumerator Freeze()
    {
        //print(Time.time);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(freezeFrameTime);
        Time.timeScale = 1f;
        //print(Time.time);
    }
}

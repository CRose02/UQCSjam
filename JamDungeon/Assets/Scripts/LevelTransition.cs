using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public AnimationCurve fadeIn;
    public AnimationCurve fadeOut;

    private float fadeTimer = 0f;
    public float fadeTime;
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float holdTimer = 0f;
    public float holdTime;
    private bool isHolding = false;

    public Color nothingColor;
    public Color fadeColor;

    public GenerateLevel generateLevel;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Begin();
            return;
        }

        if (isFadingIn)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer > fadeTime)
            {
                isFadingIn = false;
                isHolding = true;
                fadeTimer = 0f;
                LoadNext();
            }
            Color color = Color.Lerp(nothingColor, fadeColor, fadeIn.Evaluate(fadeTimer / fadeTime));
            image.color = color;
        }

        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer > holdTime)
            {
                isHolding = false;
                isFadingOut = true;
                holdTimer = 0f;
            }
            image.color = fadeColor;
        }

        if (isFadingOut)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer > fadeTime)
            {
                isFadingOut = false;
                fadeTimer = 0f;
                image.color = nothingColor;
                return;
            }
            Color color = Color.Lerp(nothingColor, fadeColor, fadeOut.Evaluate(fadeTimer / fadeTime));
            image.color = color;
        }
    }

    public void Begin()
    {
        isFadingIn = true;
        //Debug.Log("Begin");
        
        //LoadNext();
    }

    private void LoadNext()
    {
        generateLevel.GenerateNew();
    }
}

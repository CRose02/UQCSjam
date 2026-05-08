using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquash : MonoBehaviour
{
    public float xMult;
    public float yMult;
    public float interpFactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Lerp(1f, transform.localScale.x, Mathf.Pow(2f, -interpFactor * Time.deltaTime));
        float y = Mathf.Lerp(1f, transform.localScale.y, Mathf.Pow(2f, -interpFactor * Time.deltaTime));
        Vector3 newScale = new Vector3(x, y, 0f);

        transform.localScale = newScale;
    }

    public void Squash()
    {
        Vector3 newScale = new Vector3(xMult, yMult, 0f);
        transform.localScale = newScale;
    }
}

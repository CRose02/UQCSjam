using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePickup : MonoBehaviour
{
    public float startTime;
    private float startTimer = 0f;

    public GameObject[] toActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTimer += Time.deltaTime;
        if (startTimer > startTime)
        {
            foreach (GameObject obj in toActivate)
            {
                obj.SetActive(true);
            }
            this.enabled = false;
        }
    }
}

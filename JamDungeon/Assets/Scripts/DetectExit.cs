using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectExit : MonoBehaviour
{
    private ExitLevel exitLevel;

    // Start is called before the first frame update
    void Start()
    {
        exitLevel = GetComponentInParent<ExitLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (!collision.CompareTag("Player"))
        {
            return;
        }

        exitLevel.OnExit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTROsetupexit : MonoBehaviour
{
    private ExitLevel exitLevel;
    // Start is called before the first frame update
    void Start()
    {
        exitLevel = GetComponent<ExitLevel>();
        exitLevel.Setup(null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private GenerateLevel generateLevel;
    public GameObject zone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(GenerateLevel inst)
    {
        generateLevel = inst;
        GameManager.Instance.AssignZone(zone);
        //zone.se
    }

    public void OnExit()
    {
        generateLevel.GenerateNew();
    }
}

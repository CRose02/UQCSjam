using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ExitLevel : MonoBehaviour
{
    private GenerateLevel generateLevel;
    private LevelTransition levelTransition;
    public GameObject zone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(GenerateLevel inst, LevelTransition levelInst)
    {
        generateLevel = inst;
        levelTransition = levelInst;
        GameManager.Instance.AssignZone(zone);
    }

    public void OnExit()
    {
        //generateLevel.GenerateNew();
        levelTransition.Begin();
    }
}

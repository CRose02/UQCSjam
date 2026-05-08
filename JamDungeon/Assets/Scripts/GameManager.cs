using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null) Debug.Log("NO GAMEMANGER FOUND");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public int maxPlayerHealth;
    public int currentPlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

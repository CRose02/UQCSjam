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
    public bool hasKey;
    private GameObject zone;
    public GenerateLevel generateLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage()
    {
        currentPlayerHealth--;
        if (currentPlayerHealth == 0)
        {
            return true;
        }

        return false;
    }

    public void AssignZone(GameObject zoneInst)
    {
        zone = zoneInst;
    }

    public void onKeyPickup()
    {
        hasKey = true;
        zone.SetActive(true);
    }

    private int EnemyCount()
    {
        return generateLevel.enemyParent.childCount;
    }

    public bool ShouldDropKey()
    {
        if (hasKey)
        {
            return false;
        }

        int enemies = EnemyCount();
        if (enemies > 20)
        {
            return false;
        }

        if (enemies > 10)
        {
            int result = Random.Range(0, 10);
            if (result == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        if (enemies > 2)
        {
            int result = Random.Range(0, 3);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (enemies >= 0)
        {
            return true;
        }

        return false;
    }
}

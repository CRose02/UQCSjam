using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wallObj;
    public Transform levelParent;
    public Transform enemyParent;

    public int EnemySpawnCount;
    public float EnemyIncrease;
    public LayerMask wallLayer;
    public GameObject exit;

    public GameObject enemy1;
    public GameObject enemy2;

    private Transform playerTrans;

    private GameObject[,] tileGrid;

    public LevelTransition levelTransition;

    public int level = 0;

    public TextMeshProUGUI tmpLevelText;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        GameManager.Instance.generateLevel = this;
        GenerateNew();

        //triangleEnemyCountByLevel.Add(1, 2);
        //triangleEnemyCountByLevel.Add(2, 2);
        //triangleEnemyCountByLevel.Add(2, 2);
    }

    public void GenerateNew()
    {
        GameManager.Instance.hasKey = false;
        level++;
        tmpLevelText.text = $"Level: {level}";
        GameManager.Instance.ResetHealth();

        if (levelParent.childCount > 0)
        {
            List<Transform> children = new List<Transform>();
            for (int c=0; c<levelParent.childCount; c++)
            {
                children.Add(levelParent.GetChild(c));
            }

            for (int d=0; d<children.Count; d++)
            {
                Destroy(children[d].gameObject);
            }
        }

        if (enemyParent.childCount > 0)
        {
            List<Transform> children = new List<Transform>();
            for (int c = 0; c < enemyParent.childCount; c++)
            {
                children.Add(enemyParent.GetChild(c));
            }

            for (int d = 0; d < children.Count; d++)
            {
                Destroy(children[d].gameObject);
            }
        }

        int width = 60;
        int height = 60;
        float lowCutOff = -0.2f;
        float highCutOff = 0.45f;
        float scale = 0.1f;
        float densityRamp = 0.0015f;
        int seed = Random.Range(10, 100000);

        tileGrid = new GameObject[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float density = Mathf.PerlinNoise((i + seed) * scale, (j + seed) * scale);
                density -= 0.5f;
                density *= 2f;
                int x = i - width / 2;
                int y = j - height / 2;

                density += densityRamp * Mathf.Pow(new Vector2(x, y).magnitude, 2f);
                //Debug.Log(density);
                if (density > lowCutOff && density < highCutOff)
                {
                    continue;
                }
                tileGrid[i,j] = Instantiate(wallObj, new Vector2(x, y), Quaternion.identity, levelParent);
            }
        }

        for (int e=0; e<width; e++)
        {
            for (int f=0; f<height; f++)
            {
                // if not all neighbours
                if (tileGrid[e,f] == null)
                {
                    continue;
                }

                // edges
                if (e == 0 || e == width-1 || f == 0 || f == height-1)
                {
                    Destroy(tileGrid[e, f].GetComponent<ShadowCaster2D>());
                    continue;
                }

                //left
                if (tileGrid[e-1,f] == null)
                {
                    continue;
                }
                //right
                if (tileGrid[e + 1, f] == null)
                {
                    continue;
                }
                //up
                if (tileGrid[e, f+1] == null)
                {
                    continue;
                }
                //down
                if (tileGrid[e, f - 1] == null)
                {
                    continue;
                }

                Destroy(tileGrid[e, f].GetComponent<ShadowCaster2D>());
            }
        }

        // Place player
        Vector2 playerPlacement;
        int playerAttempts = 0;
        while (true)
        {
            playerPlacement = new Vector2(Random.Range(-width / 8f, width / 8f), Random.Range(-height / 8f, height / 8f));
            Collider2D[] cols = Physics2D.OverlapCircleAll(playerPlacement, 2f, wallLayer);
            playerAttempts++;

            if (cols.Length == 0 || playerAttempts > 10000)
            {
                break;
            }
            
        }
        playerTrans.position = playerPlacement;

        // Place exit
        Vector2 rayStart;
        int rayAttempts = 0;
        while (true)
        {
            rayStart = new Vector2(Random.Range(-width / 12f, width / 12f), Random.Range(-height / 12f, height / 12f));
            Collider2D[] rayCols = Physics2D.OverlapCircleAll(rayStart, 2f, wallLayer);
            rayAttempts++;

            if (rayCols.Length == 0 || rayAttempts > 10000)
            {
                break;
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(rayStart, Vector2.up, 100, wallLayer);
        GameObject exitInst = Instantiate(exit, rayHit.collider.transform.position, Quaternion.identity, levelParent);
        ExitLevel exitLevel = exitInst.GetComponentInChildren<ExitLevel>();
        exitLevel.Setup(this, levelTransition);

        // Place triangle enemies
        int numGroupsToSpawn = Mathf.Clamp(level+1, 0, 7);
        float individualOffset = 5f;
        for (int i = 0; i < numGroupsToSpawn; i++)
        {
            Vector2 attemptGroupPlacement;
            int groupAttempts = 0;

            while (true)
            {
                attemptGroupPlacement = new Vector2(Random.Range(-width / 2f, width / 2f), Random.Range(-height / 2f, height / 2f));
                Collider2D[] cols = Physics2D.OverlapCircleAll(attemptGroupPlacement, 1.2f, wallLayer);
                groupAttempts++;

                if (cols.Length == 0 || groupAttempts > 10000)
                {
                    break;
                }
            }

            for (int j = 0; j < EnemySpawnCount; j++)
            {
                Vector2 attemptPlacement;
                int enemyAttempts = 0;
                while (true)
                {
                    attemptPlacement = attemptGroupPlacement + new Vector2(Random.Range(-individualOffset, individualOffset), Random.Range(-individualOffset, individualOffset));
                    Collider2D[] cols = Physics2D.OverlapCircleAll(attemptPlacement, 1.2f, wallLayer);
                    enemyAttempts++;

                    if (cols.Length == 0 || enemyAttempts > 10000)
                    {
                        break;
                    }
                }

                Instantiate(enemy1, attemptPlacement, Quaternion.identity, enemyParent);
            }
        }

        // Place sharpshooter enemies
        int shootersToSpawn = level;
        for (int j = 0; j < shootersToSpawn; j++)
        {
            Vector2 attemptPlacement;
            int enemyAttempts = 0;
            while (true)
            {
                attemptPlacement = new Vector2(Random.Range(-width / 2f, width / 2f), Random.Range(-height / 2f, height / 2f));
                Collider2D[] cols = Physics2D.OverlapCircleAll(attemptPlacement, 1.2f, wallLayer);
                enemyAttempts++;

                if (cols.Length == 0 || enemyAttempts > 10000)
                {
                    break;
                }
            }

            Instantiate(enemy2, attemptPlacement, Quaternion.identity, enemyParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

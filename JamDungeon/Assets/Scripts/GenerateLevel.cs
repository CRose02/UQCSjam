using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wallObj;
    public Transform levelParent;

    public int EnemySpawnCount;
    public float EnemyIncrease;
    public LayerMask wallLayer;
    public GameObject exit;

    public GameObject enemy1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateNew();
    }

    public void GenerateNew()
    {
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

        int width = 80;
        int height = 80;
        float lowCutOff = -0.5f;
        float highCutOff = 0.5f;
        float scale = 0.1f;
        float densityRamp = 0.001f;
        int seed = Random.Range(10, 100000);

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
                Instantiate(wallObj, new Vector2(x, y), Quaternion.identity, levelParent);
            }
        }

        // Place exit
        //Vector2 randDir = new Vector2();
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, 100, wallLayer);
        GameObject exitInst = Instantiate(exit, rayHit.collider.transform.position, Quaternion.identity, levelParent);
        ExitLevel exitLevel = exitInst.GetComponentInChildren<ExitLevel>();
        exitLevel.Setup(this);

        for (int i = 0; i < EnemySpawnCount; i++)
        {
            Vector2 attemptPlacement;
            while (true)
            {
                attemptPlacement = new Vector2(Random.Range(-width / 2f, width / 2f), Random.Range(-height / 2f, height / 2f));
                Collider2D[] cols = Physics2D.OverlapCircleAll(attemptPlacement, 2f, wallLayer);

                if (cols.Length == 0)
                {
                    break;
                }
            }

            Instantiate(enemy1, attemptPlacement, Quaternion.identity, levelParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

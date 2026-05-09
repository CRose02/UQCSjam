using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wallObj;

    public int EnemySpawnCount;
    public float EnemyIncrease;
    public LayerMask wallLayer;
    public GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        int width = 80;
        int height = 80;
        float lowCutOff = -0.5f;
        float highCutOff = 0.5f;
        float scale = 0.1f;
        float densityRamp = 0.001f;
        int seed = Random.Range(10, 100000);

        for (int i=0; i < width; i++)
        {
            for (int j=0; j < height; j++)
            {
                float density = Mathf.PerlinNoise((i + seed) * scale, (j + seed) * scale);
                density -= 0.5f;
                density *= 2f;
                int x = i - width / 2;
                int y = j - height / 2;

                density += densityRamp * Mathf.Pow(new Vector2(x, y).magnitude, 2f);
                Debug.Log(density);
                if (density > lowCutOff && density < highCutOff)
                {
                    continue;
                }
                Instantiate(wallObj, new Vector2(x, y), Quaternion.identity);
            }
        }

        // Place exit
        //Vector2 randDir = new Vector2();
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, 100, wallLayer);
        Instantiate(exit, rayHit.collider.transform.position, Quaternion.identity);

        for (int i=0; i<EnemySpawnCount; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wallObj;

    // Start is called before the first frame update
    void Start()
    {
        int width = 100;
        int height = 100;
        float lowCutOff = -0.5f;
        float highCutOff = 0.5f;
        float scale = 0.1f;
        float densityRamp = 0.001f;

        for (int i=0; i < width; i++)
        {
            for (int j=0; j < height; j++)
            {
                float density = Mathf.PerlinNoise(i * scale, j * scale);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

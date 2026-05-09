using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform playerTrans;
    public float factor;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = playerTrans.position;

        Vector2 pos = Vector2.Lerp(transform.position, target, factor);
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }
}

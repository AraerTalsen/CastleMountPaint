using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleMove : MonoBehaviour
{
    private Rigidbody2D rb;


    public static float needleSpeed = 12f;
    public static float speedReset;

    private NeedleDestroy nd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedReset = needleSpeed;

        nd = FindObjectOfType<NeedleDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnNeedle.side == 0)
        {
            if (transform.position.x < SpawnNeedle.r.transform.position.x)
                rb.velocity = new Vector2(needleSpeed, 0);
            else
                nd.Reset();
        }
        else if (SpawnNeedle.side == 1)
        {
            if(transform.position.x > SpawnNeedle.l.transform.position.x)
                rb.velocity = new Vector2(-needleSpeed, 0);
            else
                nd.Reset();
        }
    }
}

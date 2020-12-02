using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNeedle : MonoBehaviour
{
    public GameObject Needle;

    public Transform LeftEnd;
    public Transform RightEnd;

    public static Transform l, r;

    public static int side;

    public static bool spawnedNeedle = false;

    // Start is called before the first frame update
    void Start()
    {
        l = LeftEnd;
        r = RightEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedNeedle == false)
        {
            RandomInt();
        }
    }

    void RandomInt()
    {
        side = Random.Range(0, 2);

        if (side == 0)
        {
            NeedleDestroy.canAct = true;

            spawnedNeedle = true;
            Instantiate(Needle, LeftEnd);

        }

        if (side == 1)
        {
            NeedleDestroy.canAct = true;

            spawnedNeedle = true;
            Instantiate(Needle, RightEnd);
        }

        return;
    }
}

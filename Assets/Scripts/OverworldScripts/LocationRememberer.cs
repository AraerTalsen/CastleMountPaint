using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationRememberer : MonoBehaviour
{
    public static Vector2[] pos = new Vector2[2];
    public static bool[] awokenDim = new bool[2];

    private void Awake()
    {
        pos[0] = new Vector2(0, -8);
        pos[1] = new Vector2(-13, 2);

        //awokenDim[0] = false;
        //awokenDim[1] = false;
    }
}

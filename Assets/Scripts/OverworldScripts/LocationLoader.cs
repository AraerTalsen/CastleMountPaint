using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    public int num;
    public Transform player;

    //private AreaLoader al;

    private void Start()
    {
        if (LocationRememberer.awokenDim[num])
        {
            player.position = LocationRememberer.pos[num];
            //al = FindObjectOfType<AreaLoader>();
            //al.delay = true;
        }
        else
        {
            LocationRememberer.awokenDim[num] = true;
            print("good");
        }   
    }
}

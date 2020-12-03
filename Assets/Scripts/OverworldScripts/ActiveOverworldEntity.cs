using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOverworldEntity : MonoBehaviour
{
    public static int[] entityCount = { 3 };

    public static List<GameObject>[][] entityInDimension =
    {
        new List<GameObject>[]//Level 1
        {
            new List<GameObject>(),//Enemies
            new List<GameObject>() //Assets
        }
    };
}

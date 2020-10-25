using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersonality : MonoBehaviour
{

    public enum personality { INSPIRING, CARING, ANGRY, CUNNING};


    public static string RandomAssignPersonality()
    {
        personality p = (personality)Random.Range(0, 4);

        switch (p)
        {
            case personality.INSPIRING:
            {
                return "Inspiring";
            }
            case personality.CARING:
            {
                return "Caring";
            }
            case personality.CUNNING:
            {
                return "Cunning";
            }
            case personality.ANGRY:
            {
                return "Angry";
            }
            default:
            {
                return "Angry";
            }
        }

    }


    public static int[] AssignPersonalityWeights(string p)
    { 
        int[] weightRange;

        switch(p)
        {
            case "Inspiring":
            {
                weightRange = new int[]{ 69, 81, 92, 99};
                return weightRange;
            }
            case "Caring":
            {
                weightRange = new int[]{ 81, 69, 99, 92};
                return weightRange;
            }
            case "Cunning":
            {
                weightRange = new int[]{ 99, 81, 92, 69};
                return weightRange;
            }
            case "Angry":
            {
                weightRange = new int[]{ 81, 92, 69, 99};
                return weightRange;
            }
            default:
            {
                weightRange = new int[] { 81, 92, 69, 99 };
                return weightRange;
            }
        }
    }
}

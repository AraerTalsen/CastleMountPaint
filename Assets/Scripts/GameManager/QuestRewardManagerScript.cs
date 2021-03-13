using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardManagerScript : MonoBehaviour
{
    public GameObject sketchQuestRewardOne;
    public GameObject sketchQuestRewardTwo;
    public GameObject sketchQuestRewardThree;

    public static bool helpfulNPCReward = false;


    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.barrelQuestItemGiven == true)
        {
            if(sketchQuestRewardOne != null)
            {
                sketchQuestRewardOne.SetActive(true);
            } else
            {
                sketchQuestRewardOne = null;
                sketchQuestRewardOne.SetActive(false);
            }
            
        }

        if(helpfulNPCReward == true)
        {
            sketchQuestRewardTwo.SetActive(true);
        }
    }
}

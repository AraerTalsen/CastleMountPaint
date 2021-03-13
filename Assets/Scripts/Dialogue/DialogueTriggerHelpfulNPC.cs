using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerHelpfulNPC : Interactable
{

    public DialogueBase DB1, DB2;

    private int timesTalked = 0;

    public override void Interact()
    {
        Debug.Log("Interacted");
        if(timesTalked == 0)
        {
            DialogueManager.instance.EnqueueDialogue(DB1);
            timesTalked = 1;
            QuestRewardManagerScript.helpfulNPCReward = true;
        }
        else
        {
            DialogueManager.instance.EnqueueDialogue(DB2);
        }
    }
}

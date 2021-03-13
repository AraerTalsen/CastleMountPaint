using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNPCBarrel : Interactable
{
    public DialogueBase DB1, DB2, DB3, DB4;

    private int changeDialogue = 0;

    public override void Interact()
    {
        Debug.Log("Interacted");

        if (PlayerMovement.barrelQuestItemPickedUp == false)
        {
            if(PlayerMovement.barrelQuestItemGiven == true)
            {
                DialogueManager.instance.EnqueueDialogue(DB3);
            } else if(PlayerMovement.barrelQuestItemGiven == false && changeDialogue == 0)
            {
                DialogueManager.instance.EnqueueDialogue(DB1);
                changeDialogue = 1;
            }
            else
            {
                DialogueManager.instance.EnqueueDialogue(DB4);
            }
        } else if (PlayerMovement.barrelQuestItemPickedUp == true)
        {
            PlayerMovement.barrelQuestItemGiven = true;
            PlayerMovement.barrelQuestItemPickedUp = false;
            DialogueManager.instance.EnqueueDialogue(DB2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMultipleLines : Interactable
{
    public DialogueBase DB1, DB2, DB3;

    public override void Interact()
    {
        Debug.Log("Interacted");

        if (PlayerMovement.barrelQuestItemPickedUp == false)
        {
            if(PlayerMovement.barrelQuestItemGiven == true)
            {
                DialogueManager.instance.EnqueueDialogue(DB3);
            } else
            {
                DialogueManager.instance.EnqueueDialogue(DB1);
            }
        } else if (PlayerMovement.barrelQuestItemPickedUp == true)
        {
            PlayerMovement.barrelQuestItemGiven = true;
            PlayerMovement.barrelQuestItemPickedUp = false;
            DialogueManager.instance.EnqueueDialogue(DB2);
        }
    }
}

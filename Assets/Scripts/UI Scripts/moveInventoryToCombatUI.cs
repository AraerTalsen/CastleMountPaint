using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveInventoryToCombatUI : MonoBehaviour
{
    public void selectButtonToMove()
    {
        ListCreator.moveButtonToCombat = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAndRemoveButtonTest : MonoBehaviour
{
    public Inventory inventory;
    public Sprite rock, test;

    // Start is called before the first frame update
    public void AddToInv()
    {
        inventory.AddItem(rock);
        inventory.AddItem(test);
        
    }

    public void RemoveFromInv()
    {
        inventory.RemoveItem(rock);
    }

}

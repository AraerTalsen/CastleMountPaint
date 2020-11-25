using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Sprite blankSprite;
    public Image[] inventory;
    List<Sprite> inventoryList = new List<Sprite>();

    public void AddItem(Sprite item)
    {
        inventoryList.Add(item);
        UpdateItems();
    }

    public void RemoveItem(Sprite item)
    {
        inventoryList.Remove(item);
        UpdateItems();
    }

    public void UpdateItems()
    {
        // filling inventory with blanks
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].sprite = blankSprite;
            
        }

        // filling inventory with the list
        for (int i = 0; i < inventoryList.Count; i++)
        {
            inventory[i].sprite = inventoryList[i];
        }
    }

    public bool ContainsItem(Sprite spriteToCheck)
    {
        int itemIndex = inventoryList.IndexOf(spriteToCheck);
        return (itemIndex != -1);
    }

}

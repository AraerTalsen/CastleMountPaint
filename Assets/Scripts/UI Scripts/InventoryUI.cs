using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] CombatMinionInventory;

    public Button[] buttons;

    public GameObject inventoryUIObject;

    public static bool inventoryOpen = true;
    public static bool buttonPressed = false;

    public static bool minion1Removed = false;
    public static bool minion2Removed = false;
    public static bool minion3Removed = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen == false)
        {
            inventoryOpen = true;
            inventoryUIObject.SetActive(true);


        } else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen == true)
        {
            inventoryOpen = false;
            inventoryUIObject.SetActive(false);
            
        }

        if (ListCreator.numMinionInCombat == 1)
        {
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
        }
        if (ListCreator.numMinionInCombat == 2)
        {
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
        }
        if (ListCreator.numMinionInCombat == 3)
        {
            CombatMinionInventory[2].GetComponent<Image>().enabled = true;
        }
    }

    public void NonCombatToCombatButton(Button button)
    {

    }


    public void CombatToNonCombatButton(Button button)
    {
        

       
    }

}

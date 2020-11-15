using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] NonCombatMinionInventory;
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

            
                if (PlayerMinionInventory.sketch1Active == true)
                {
                    NonCombatMinionInventory[0].GetComponent<Image>().enabled = true;
                }
                if (PlayerMinionInventory.sketch2Active == true)
                {
                    NonCombatMinionInventory[1].GetComponent<Image>().enabled = true;
                }
                if (PlayerMinionInventory.sketch3Active == true)
                {
                    NonCombatMinionInventory[2].GetComponent<Image>().enabled = true;
                }

            for (int i = 0; i < CombatMinionInventory.Length; i++)
            {
                CombatMinionInventory[i].GetComponent<Image>().enabled = false;
            }

        } else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen == true)
        {
            inventoryOpen = false;
            inventoryUIObject.SetActive(false);
            
        }
    }

    public void NonCombatToCombatButton(Button button)
    {
        if (button.name == "InventoryMinionSlot1" && PlayerMinionInventory.sketch1Active == true) {
            buttonPressed = true;
            minion1Removed = true;
        }

        if (button.name == "InventoryMinionSlot2" && PlayerMinionInventory.sketch2Active == true)
        {
            buttonPressed = true;
            minion2Removed = true;
        }

        if (button.name == "InventoryMinionSlot3" && PlayerMinionInventory.sketch3Active == true)
        {
            buttonPressed = true;
            minion3Removed = true;
        }
    }


    public void CombatToNonCombatButton(Button button)
    {
        if (button.name == "CombatMinionSlot1" && minion1Removed && buttonPressed)
        {
            minion1Removed = false;
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[0].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch1Active = false;
        } else if (button.name == "CombatMinionSlot2" && minion1Removed && buttonPressed)
        {
            minion1Removed = false;
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[0].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch1Active = false;
        } else if (button.name == "CombatMinionSlot3" && minion1Removed && buttonPressed)
        {
            minion1Removed = false;
            CombatMinionInventory[2].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[0].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch1Active = false;
        }

        if (button.name == "CombatMinionSlot1" && minion2Removed && buttonPressed)
        {
            minion2Removed = false;
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[1].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch2Active = false;
        }
        else if (button.name == "CombatMinionSlot2" && minion2Removed && buttonPressed)
        {
            minion2Removed = false;
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[1].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch2Active = false;
        }
        else if (button.name == "CombatMinionSlot3" && minion2Removed && buttonPressed)
        {
            minion2Removed = false;
            CombatMinionInventory[2].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[1].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch2Active = false;
        }

        if (button.name == "CombatMinionSlot1" && minion3Removed && buttonPressed)
        {
            minion3Removed = false;
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[2].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch3Active = false;
        }
        else if (button.name == "CombatMinionSlot2" && minion3Removed && buttonPressed)
        {
            minion3Removed = false;
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[2].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch3Active = false;
        }
        else if (button.name == "CombatMinionSlot3" && minion3Removed && buttonPressed)
        {
            minion3Removed = false;
            CombatMinionInventory[2].GetComponent<Image>().enabled = true;
            NonCombatMinionInventory[2].GetComponent<Image>().enabled = false;
            PlayerMinionInventory.sketch3Active = false;
        }

        buttonPressed = false;
    }

}

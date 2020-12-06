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

    public bool openInvOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(FixInvBug());

    }

    IEnumerator FixInvBug()
    {
        inventoryOpen = true;
        inventoryUIObject.SetActive(true);
        inventoryUIObject.transform.localPosition = new Vector2(1000, 1000);
        yield return new WaitForEndOfFrame();
        inventoryOpen = false;
        inventoryUIObject.transform.localPosition = new Vector2(0, 0);
        inventoryUIObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && inventoryOpen == false)
        {
            PlayerMovement.pauseGame = true;
            inventoryOpen = true;
            inventoryUIObject.SetActive(true);


        } else if (Input.GetKeyUp(KeyCode.Tab) && inventoryOpen == true)
        {
            PlayerMovement.pauseGame = false;
            inventoryOpen = false;
            inventoryUIObject.SetActive(false);
            
        }

        if (ListCreator.numMinionInCombat == 0)
        {
            CombatMinionInventory[0].GetComponent<Image>().enabled = false;
            CombatMinionInventory[1].GetComponent<Image>().enabled = false;
            CombatMinionInventory[2].GetComponent<Image>().enabled = false;
        }
        if (ListCreator.numMinionInCombat == 1)
        {
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            CombatMinionInventory[1].GetComponent<Image>().enabled = false;
            CombatMinionInventory[2].GetComponent<Image>().enabled = false;
        }
        if (ListCreator.numMinionInCombat == 2)
        {
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
            CombatMinionInventory[2].GetComponent<Image>().enabled = false;
        }
        if (ListCreator.numMinionInCombat == 3)
        {
            CombatMinionInventory[0].GetComponent<Image>().enabled = true;
            CombatMinionInventory[1].GetComponent<Image>().enabled = true;
            CombatMinionInventory[2].GetComponent<Image>().enabled = true;
        }
    }

}

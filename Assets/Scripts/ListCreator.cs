using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListCreator : MonoBehaviour
{
    //List system based on tutorial found here: https://www.codeneuron.com/creating-a-dynamic-scrollable-list-in-unity/ 

    public Sprite image1, image2, image3;

    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    [SerializeField]
    private int numberOfItems = 0;
    public static int numberOfItemsCollected = 0;

    public List<string> itemNames = new List<string>();
    public List<Sprite> itemImages = new List<Sprite>();
    public List<GameObject> itemHolder = new List<GameObject>();
    public static List<string> combatMinionsList = new List<string>();

    public static bool runInventoryUpdate = false;
    public static bool moveButtonToCombat = false;
    public static bool addToCombatInventory = false;
    public static int numMinionInCombat = 0;

    public int throwAwayIntForTesting = 1;

    public static bool minion1Active = false;
    public static bool minion2Active = false;
    public static bool minion3Active = false;

    public static bool callMinion1 = true;
    public static bool callMinion2 = true;
    public static bool callMinion3 = true;

    void Start()
    {
        if (itemNames != null)
        {
            itemNames = GlobalControl.Instance.itemNames;
        }
        if (itemImages != null)
        {
            itemImages = GlobalControl.Instance.itemImages;
        }
        if (itemHolder != null)
        {
            itemHolder = GlobalControl.Instance.itemHolder;
        }
        if (combatMinionsList != null)
        {
            combatMinionsList = GlobalControl.Instance.combatMinionsList;
        }

    }


    void Update()
    {
        numberOfItems = numberOfItemsCollected;

        if (runInventoryUpdate == true)
        {
            UpdateMinionInventory();
            runInventoryUpdate = false;
            SaveInventoryDataAcrossScenes();
        }

        if(moveButtonToCombat == true)
        {
            //Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<ItemDetails>().text.text);
            string s = EventSystem.current.currentSelectedGameObject.GetComponent<ItemDetails>().text.text;
            GameObject g = itemHolder[itemHolder.IndexOf(EventSystem.current.currentSelectedGameObject)];
            if (s == "Dan")
            {
                combatMinionsList.Add("Dan");
            } else if (s == "Mike")
            {
                combatMinionsList.Add("Mike");
            } else if (s == "Sean")
            {
                combatMinionsList.Add("Sean");
            }

            if (combatMinionsList.Contains("Dan") && callMinion1 == true)
            {
                minion1Active = true;
            }

            if (combatMinionsList.Contains("Mike") && callMinion2 == true)
            {
                minion2Active = true;
            }

            if (combatMinionsList.Contains("Sean") && callMinion3 == true)
            {
                minion3Active = true;
            }


            itemNames.Remove(EventSystem.current.currentSelectedGameObject.GetComponent<ItemDetails>().text.text);
            itemImages.Remove(EventSystem.current.currentSelectedGameObject.GetComponent<ItemDetails>().image.sprite);
            itemHolder.Remove(EventSystem.current.currentSelectedGameObject);
            Destroy(g);
            addToCombatInventory = true;
            numMinionInCombat++;
            numberOfItems--;
            numberOfItemsCollected--;
            moveButtonToCombat = false;
            UpdateMinionInventory();

        }

    }

    public void UpdateMinionInventory()
    {
        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, numberOfItems * 50);
        int f = itemHolder.Count;
        for (int i = 0; i < f; i++)
        {
            GameObject g = itemHolder[0];
            itemHolder.Remove(g);
            Destroy(g);
        }
        

        for (int i = 0; i < numberOfItems; i++)
        {
            // by height of item
            float spawnY = i * 50;
            //newSpawn Position
            Vector3 pos = new Vector3(25, -spawnY, SpawnPoint.position.z);
            //instantiate item
            itemHolder.Insert(0, Instantiate(item, pos, SpawnPoint.rotation));
            //setParent
            itemHolder[0].transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ItemDetails itemDetails = itemHolder[0].GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = itemNames[i];
            //set image
            itemDetails.image.sprite = itemImages[i];
        }
    }

    //adds minion to inventory on collision
    public void InsertNewMinion()
    {
        if (throwAwayIntForTesting == 1)
        {
            itemNames.Insert(0, "Dan");
            itemImages.Insert(0, image1);
            throwAwayIntForTesting++;
        } else if (throwAwayIntForTesting == 2)
        {
            itemNames.Insert(0, "Mike");
            itemImages.Insert(0, image2);
            throwAwayIntForTesting++;
        } else if (throwAwayIntForTesting == 3)
        {
            itemNames.Insert(0, "Sean");
            itemImages.Insert(0, image3);
        }

    }

    public void SaveInventoryDataAcrossScenes()
    {
        GlobalControl.Instance.itemNames = itemNames;
        GlobalControl.Instance.itemImages = itemImages;
        GlobalControl.Instance.itemHolder = itemHolder;
        GlobalControl.Instance.combatMinionsList = combatMinionsList;
    }

}

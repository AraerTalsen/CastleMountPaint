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

    public static bool runInventoryUpdate = false;
    public static bool moveButtonToCombat = false;
    public static bool addToCombatInventory = false;
    public static int numMinionInCombat = 0;

    public int throwAwayIntForTesting = 1;

    void Update()
    {
        numberOfItems = numberOfItemsCollected;

        if (runInventoryUpdate == true)
        {
            UpdateMinionInventory();
            runInventoryUpdate = false;
        }

        if(moveButtonToCombat == true)
        {
            string n = EventSystem.current.currentSelectedGameObject.GetComponent<ItemDetails>().text.text;
            Debug.Log(n);
            /*
            if (n == "Dan")
            {
                itemHolder.Remove(item);
                itemNames.Remove(n);
                itemImages.Remove(image1);
                itemHolder.IndexOf(n);
            } else if (n == "Mike")
            {
                itemHolder.Remove(item);
                itemNames.Remove(n);
                itemImages.Remove(image2);
            } else if (n == "Sean")
            {
                itemHolder.Remove(item);
                itemNames.Remove(n);
                itemImages.Remove(image3);
            }
            */
            //right now only removes first item in list but I physically can't work on this anymore my brain has died
            GameObject g = itemHolder[0];
            itemHolder.RemoveAt(0);
            itemNames.RemoveAt(0);
            itemImages.RemoveAt(0);
            Destroy(g);
            addToCombatInventory = true;
            numMinionInCombat++;
            numberOfItemsCollected--;
            moveButtonToCombat = false;
        }

    }

    public void UpdateMinionInventory()
    {
        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, numberOfItems * 50);

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

}

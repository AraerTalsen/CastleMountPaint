using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitTextUI : MonoBehaviour
{

    public TextMeshProUGUI hitText;

    //public GameObject missText;
    //public GameObject hitText;
    //public GameObject critText;

    //public Transform spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        hitText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedleDestroy.miss)
        {
            //Instantiate(missText, spawnArea);
            hitText.text = "miss";
        }
        if (NeedleDestroy.hit)
        {
            //Instantiate(hitText, spawnArea);
            hitText.text = "hit";
        }
        if (NeedleDestroy.crit)
        {
            //Instantiate(critText, spawnArea);
            hitText.text = "crit";
        }
    }
}

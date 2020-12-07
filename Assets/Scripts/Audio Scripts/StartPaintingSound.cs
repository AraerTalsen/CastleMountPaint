using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPaintingSound : MonoBehaviour
{
    //public static bool isActive = false;

    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/StartPainting");
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isActive)
        //{
        //    gameObject.SetActive(true);
        //}
    }
}

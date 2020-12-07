﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject audioMenu;
    public bool menuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        audioMenu.SetActive(false);
        menuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Time.timeScale = 1;
            PlayerMovement.pauseGame = false;
            OpenMenu();
        }

        if(menuOpen == true)
        {
            audioMenu.SetActive(true);
        }
        else
        {
            audioMenu.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        if (menuOpen)
        {
            menuOpen = false;
        }
        else if (!menuOpen)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Pause");
            Time.timeScale = 0;
            PlayerMovement.pauseGame = true;
            menuOpen = true;
        }
    }
}

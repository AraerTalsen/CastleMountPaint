using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        if (!gamePaused)
        {
            Time.timeScale = 1;
        }
       
    }
}

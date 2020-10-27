using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a jank garbage script that serves as a temp fix for enemies not dying for tomorrow's playtest
public class DisableEnemies : MonoBehaviour
{
    public GameObject enemy1Assets, enemy2Assets, enemy3Assets;

    // Update is called once per frame
    void Update()
    {
        if (CombatSystem.enemy1Dead == true)
        {
            enemy1Assets.gameObject.SetActive(false);
        }
        if (CombatSystem.enemy2Dead == true)
        {
            enemy2Assets.gameObject.SetActive(false);
        }
        if (CombatSystem.enemy3Dead == true)
        {
            enemy3Assets.gameObject.SetActive(false);
        }
    }
}

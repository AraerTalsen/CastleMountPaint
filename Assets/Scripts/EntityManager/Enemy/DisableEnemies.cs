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
        if (CombatSystem.enemyParty[0].isDead == true)
        {
            enemy1Assets.gameObject.SetActive(false);
        }
        if (CombatSystem.enemyParty[1].isDead == true)
        {
            enemy2Assets.gameObject.SetActive(false);
        }
        if (CombatSystem.enemyParty[2].isDead == true)
        {
            enemy3Assets.gameObject.SetActive(false);
        }
    }
}

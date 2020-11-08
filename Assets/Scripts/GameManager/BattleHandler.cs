using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public static Enemy[] EnemyParty()
    {
        int r = Random.Range(1, 4);

        Enemy[] e = new Enemy[3];

        int options = EnemyLibrary.size;

        if (PlayerMovement.enemy1Combat == true)
        {
            for (int i = 0; i < 3; i++) e[i] = EnemyLibrary.ChooseEnemy(Random.Range(0, 2));
            return e;

        } else if (PlayerMovement.enemy2Combat == true)
        {
            for (int i = 0; i < 3; i++) e[i] = EnemyLibrary.ChooseEnemy(Random.Range(1, 3));
            return e;

        } else if (PlayerMovement.enemy3Combat == true)
        {
            for (int i = 0; i < 3; i++) e[i] = EnemyLibrary.ChooseEnemy(Random.Range(0, 3));
            return e;

        } else {

            for (int i = 0; i < 3; i++) e[i] = EnemyLibrary.ChooseEnemy(Random.Range(0, options));
            return e;
        }


    }
}

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

        for (int i = 0; i < 3; i++) e[i] = EnemyLibrary.ChooseEnemy(Random.Range(0, options));

        return e;
    }
}

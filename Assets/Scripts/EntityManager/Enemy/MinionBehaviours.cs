using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinionBehaviours : MonoBehaviour
{
    //Minion components
    public static int numMinions = 0;
    public Image[] minionHUDS;
    public Vector2[] spawnPoints;
    public GameObject minionBody;
    private GameObject[] minionBodies = new GameObject[3];
    public Enemy[] minions = new Enemy[3];

    //Accessed libraries
    private PlayerButtons pb;
    private CombatSystem cs;
    private UpdateHUD uh;

    private void Start()
    {
        pb = FindObjectOfType<PlayerButtons>();
        cs = FindObjectOfType<CombatSystem>();
        uh = FindObjectOfType<UpdateHUD>();
    }

    public void NewMinion()
    {
        minions[numMinions] = Instantiate(EnemyLibrary.ChooseEnemy(Random.Range(0, 3))); //Minion brain is created

        numMinions++;
        pb.SetAllyToButtons(numMinions);
        uh.AddAlly(minions[numMinions - 1]);

        //hard set minion attack value
        minions[numMinions - 1].HitValue = 1;

        CombatSystem.allyParty[numMinions] = minions[numMinions - 1];
    }

    public void MinionTurn(int index, Enemy[] e, Entity[]a)
    {
        if (index <= numMinions)
        {
            pb.PlayerNewTurn(index, e, a);
            cs.EnemyDeadCheck();
        }
        else
        {
            cs.EnemyDeadCheck();
            Invoke("EnemyTurn", 1); //switch to the Enemy Turn Function with a small delay
        }
    }

    //Invoke can only be called on a method in the same class, but Enemy Turn is in a different class.
    public void EnemyTurn() { cs.EnemyTurn(); } 
}

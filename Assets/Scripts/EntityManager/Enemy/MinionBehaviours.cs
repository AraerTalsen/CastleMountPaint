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

    private void Start()
    {
        pb = FindObjectOfType<PlayerButtons>();
        cs = FindObjectOfType<CombatSystem>();
    }

    public void NewMinion()
    {
        minionHUDS[numMinions].gameObject.SetActive(true); //Minion GUI
        minions[numMinions] = EnemyLibrary.ChooseEnemy(Random.Range(0, 3)); //Minion brain is created

        //Minion visual appears
        minionBodies[numMinions] = Instantiate(minionBody, spawnPoints[numMinions], Quaternion.identity);

        minionBodies[numMinions].GetComponent<SpriteRenderer>().sprite = minions[numMinions].enemySprite;
        minionBodies[numMinions].transform.localScale = new Vector3(-1, 1, 1);
        numMinions++;
        pb.SetAllyToButtons(numMinions);

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
        else Invoke("EnemyTurn", 1); //switch to the Enemy Turn Function with a small delay
    }

    //Invoke can only be called on a method in the same class, but Enemy Turn is in a different class.
    public void EnemyTurn() { cs.EnemyTurn(); } 
}

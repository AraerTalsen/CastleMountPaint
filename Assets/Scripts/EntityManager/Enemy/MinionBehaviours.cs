using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinionBehaviours : MonoBehaviour
{
    public int numMinions = 0;
    //This is to check to see if minions are summoned to start new minion state, needs its own variable to send
    public static int numMinionsSendable = 0;
    public Image[] minionHUDS;
    public GameObject minionBody;
    public Vector2[] spawnPoints;
    public Enemy[] minions = new Enemy[3];
    private GameObject[] minionBodies = new GameObject[3];

    public void NewMinion()
    {
        print(minionHUDS[numMinions].name);
        minionHUDS[numMinions].gameObject.SetActive(true); //Minion GUI
        minions[numMinions] = EnemyLibrary.ChooseEnemy(Random.Range(0, 3)); //Minion brain is created

        //Minion visual appears
        minionBodies[numMinions] = Instantiate(minionBody, spawnPoints[numMinions], Quaternion.identity);

        minionBodies[numMinions].GetComponent<SpriteRenderer>().sprite = minions[numMinions].enemySprite;
        minionBodies[numMinions].transform.localScale = new Vector3(-1, 1, 1);
        numMinions++;
        numMinionsSendable = numMinions;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinionBehaviours : MonoBehaviour
{
    public static int numMinions = 0;
    public Image[] minionHUDS;
    public GameObject minionBody;
    public Vector2[] spawnPoints;
    public Enemy[] minions = new Enemy[3];
    private GameObject[] minionBodies = new GameObject[3];
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
        print(minions[numMinions - 1].HitValue + " " + minions[numMinions - 1].isDead + " " + minions[numMinions - 1].maxHP + " " + minions[numMinions - 1].currentHP);
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

    public void EnemyTurn() { cs.EnemyTurn(); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerMoves : MonoBehaviour
{
    public Button[] actions;
    public Image[] minionHUDS;
    public Vector2[] spawnPoints;
    public GameObject minionBody;
    //Select Target
    public Button[] enemySelect;
    private Enemy[] minions = new Enemy[3];
    private GameObject[] minionBodies = new GameObject[3];
    private int numMinions = 0;
    private Player p;
    private Enemy[] e;
    private CombatSystem cs;
    private EnemyMoves em;

    public void Start()
    {
        cs = FindObjectOfType<CombatSystem>();
        em = FindObjectOfType<EnemyMoves>();
    }

    //Player Decision UI elements pop up
    public void PlayerDecision(Player player, Enemy[]enemies)
    {
        //Debug.Log("Start Player Turn");

        p = player;
        e = enemies;

        SetButtonsActive(true);

        bool isMaxAllies = numMinions < 3;
        actions[1].gameObject.SetActive(isMaxAllies);
    }

    //Player actions based on button number selected
    public void OnActionSelect()
    {
        string pos = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().name;
        int num;
        int.TryParse(pos, out num);

        SetButtonsActive(false);

        switch(num)
        {
            case 0:
            {
                SelectTarget(); //switch to the Choose Target Function
                break;
            }
            case 1:
            {
                //needs to create new set of functions for summoning (do we want multiple summon types, how many?)
                StartCoroutine(SummonPlayerAlly()); //switch to the summon ally function
                break;
            }
            case 2:
            {
                StartCoroutine(SketchEnemies());
                break;
            }
            case 3:
            {
                StartCoroutine(HealPlayerAllies());//start the heal allies coroutine
                break;
            }
            default:
            {
                SelectTarget(); //switch to the Choose Target Function
                break;
            }

        }
    }

    //Select an Enemy to Target
    void SelectTarget()
    {
        Debug.Log("Select an Enemy");

        for (int i = 0; i < e.Length; i++)
        {
            if (!e[i].isDead) enemySelect[i].gameObject.SetActive(true);
        }
    }

    public void EnemySelectButton()
    {
        for (int i = 0; i < enemySelect.Length; i++) enemySelect[i].gameObject.SetActive(false); //hide all the buttons 

        int select;
        int.TryParse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name, out select);
        Debug.Log("Enemy " + select + " Targeted");
        e[select].targeted = true; //Target Toggle on Enemy Object is set to true
        StartCoroutine(PlayerAttack()); //switch to Player Attack
    }

    //Used to delay the Player's Attack
    IEnumerator PlayerAttack()
    {
        print("Attack");
        yield return new WaitForSeconds(2f);
        //Damage Enemy
        DamageEnemy(); //switches to the Damage Enemy function
    }

    //Used to delay the Player's Heal
    IEnumerator HealPlayerAllies()
    {
        yield return new WaitForSeconds(2f);
        //Heal Player (can be adjusted to work on summoned allies)
        HealAllies(); //switches to the Damage Enemy function
    }

    //Used to delay the Player's Attack
    IEnumerator SketchEnemies()
    {
        print("Sketch");
        yield return new WaitForSeconds(2f);
        //Add "sketch the enemies" functinality
        MinionTurn();
    }

    //Used to delay the Player's Attack
    IEnumerator SummonPlayerAlly()
    {
        yield return new WaitForSeconds(2f);
        //Summon Ally for player
        SummonAllies(); //switches to the Damage Enemy function
    }

    //Checks which enemy was targeted and deals the correct damage
    public void DamageEnemy()
    {
        for (int i = 0; i < e.Length; i++)
        {
            if (e[i].targeted == true) //if the enemy is targeted
            {
                Debug.Log("Enemy " + i + " has been Damaged");

                e[i].currentHP -= p.HitValue; //apply the player's hit value
                Mathf.Clamp(e[i].currentHP, 0, e[i].maxHP);
                e[i].targeted = false; //Target Toggle switched to false
            }
            if (!e[i].isDead && e[i].currentHP <= 0) //if the enemy's health is less than or equal to 0
            {
                //Kill enemy
                Debug.Log("Enemy " + i + " is Dead");
                e[i].isDead = true; //isDead toogle on Enemy Object is toggled on
                CombatSystem.livingEnemies--;
            }
        }
        cs.EnemyDeadCheck();
    }

    void HealAllies()
    {
        print("Heal");
        p.currentHP += p.HitValue;
        if (p.currentHP >= p.maxHP)
        {
            p.currentHP = p.maxHP;
        }
        MinionTurn();
    }

    void SummonAllies()
    {
        //print("Summon");
        minionHUDS[numMinions].gameObject.SetActive(true);
        minions[numMinions] = EnemyLibrary.ChooseEnemy(Random.Range(0, 3));
        minionBodies[numMinions] = Instantiate(minionBody, spawnPoints[numMinions], Quaternion.identity);

        minionBodies[numMinions].GetComponent<SpriteRenderer>().sprite = minions[numMinions].enemySprite;
        minionBodies[numMinions].transform.localScale = new Vector3(-1, 1, 1);
        minions[numMinions].isDead = false;
        minions[numMinions].personality = EnemyPersonality.RandomAssignPersonality();
        minions[numMinions].pWeightRange = EnemyPersonality.AssignPersonalityWeights(minions[numMinions].personality);
        numMinions++;

        //Debug.Log("Summoned a minion");
        MinionTurn();
    }

    void MinionTurn()
    {
        //Debug.Log("Start Minion Turn");
        for (int i = 0; i < numMinions; i++)
        {
            //if (!minions[i].isDead) em.ChooseAction(e[Random.Range(0, e.Length)], minions[i], minions);
        }
        cs.EnemyDeadCheck(); //switches to Player Dead Check
    }

    //Set accessibility of action buttons
    private void SetButtonsActive(bool isActive)
    {
        for (int i = 0; i < actions.Length; i++) actions[i].gameObject.SetActive(isActive);
    }
}

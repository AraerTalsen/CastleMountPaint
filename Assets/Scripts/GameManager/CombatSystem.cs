﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatSystem : MonoBehaviour
{
    //Enemy ID
    public static int id;

    //Debug functionality
    public Enemy[] dE;
    public Enemy[] dA;

    //Combative parties
    public Player player1; 

    public static Enemy[] enemyParty; 
    public static Entity[] allyParty = new Entity[4];

    //Entity placements
    public Transform playerSpawn; 
    public Transform[] pos; //Spawn points for enemies

    //Party UI

    public GameObject[] eDisplay; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public GameObject[] aDisplay; //The panel that ally info is listed on. [Disable to make everything disabled.]
    public Image[] enemyAction;
    public Sprite[] actions;

    public static int livingEnemies;
    private bool debugSession = false;

    //Accessed classes
    private EnemyMoves em;
    private PlayerMoves pm;
    private UpdateHUD uh;
    private PlayerButtons pb;

    public GameObject[] img;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyParty == null || enemyParty.Length == 0)
        {
            enemyParty = new Enemy[dE.Length];

            for (int i = 0; i < dE.Length; i++)
            {
                enemyParty[i] = Instantiate(dE[i]);
                enemyParty[i].currentHP = 1;
            }
            debugSession = true;
        }
        else debugSession = false;

        livingEnemies = enemyParty.Length;
        
        em = FindObjectOfType<EnemyMoves>();
        pm = FindObjectOfType<PlayerMoves>();
        uh = FindObjectOfType<UpdateHUD>();
        pb = FindObjectOfType<PlayerButtons>();

        SetUpCombat();
    }

    private void SetUpCombat()
    {
        List<string> s = ListCreator.combatMinionsList;
        
        allyParty[0] = player1;
        ((Player)allyParty[0]).currentPaint = ((Player)allyParty[0]).maxPaint;

        if (s == null || debugSession)
        {
            MinionBehaviours.numMinions = 3;
            print("Debug party active");
            for (int i = 1; i < allyParty.Length; i++)
            {
                allyParty[i] = Instantiate(dA[i - 1]);
            }                  
        }
        else
        {
            MinionBehaviours.numMinions = s.Count;
            for (int i = 1; i <= s.Count; i++)
            {
                allyParty[i] = Instantiate((Entity)Resources.Load("Enemies/" + s[i - 1], typeof(Object)));
            }
        }
        

        uh.LoadHUDs();
        uh.UpdateEveryHUD();

        PlayerTurn();
    }


    /*
        Combat cycle is: Player turn, Minion turn, Enemy turn. Check if attacked party has dead members 
        after each opposing party member attacks.
    */

    public void StateMachine()
    {
        /*Entity turn*/
        //e.statusEffect[0].Effect();
        //ChooseMove()
        //e.statusEffect[1].Effect();
        //uh.UpdateEveryHUD();


        /*Check if combat is over*/
    }

    private void ChooseMove()
    {
        pb.LoadMoves(enemyParty[0].moveList);
        //LoadMoves(e.moves)
            //Open buttons
            //When a move is chosen, open 'select target'
    }

    private void SelectTarget()
    {
        //Move gives a key for targets
        //if key == friends[x] -> friend buttons until x
        //else if key == foes[x] -> foe buttons until x
        //else if key == all[x] -> friend and foe buttons until x
        //else -> load predetermined targets

        //move.Load(targets)
    }

    private void PlayerTurn()
    {
        pm.PlayerDecision(allyParty, enemyParty);
    }

    public void EnemyDeadCheck()
    {
        for (int i = 0; i < enemyParty.Length; i++)
            if (enemyParty[i].currentHP <= 0 && !enemyParty[i].isDead)
            {
                enemyParty[i].isDead = true;
                eDisplay[i].SetActive(false);
                livingEnemies--;
            }

        uh.UpdateEveryHUD();

        if (livingEnemies <= 0)
        {
            EndCombat(true);
        }
    }

    //this could later be used to decide what attacks the enemy is doing
    public IEnumerator EnemyTurn()
    {
        for(int i = 0; i < enemyParty.Length; i++)
        {
            if(!enemyParty[i].isDead)
            {
                img[i].GetComponent<enemyCombatAnim>().AnimTime();
                yield return new WaitForSeconds(1f);
                //Enemy move is decided if enemy is alive
                enemyAction[i].sprite = ImageAssign(em.ChooseAction(enemyParty[i]));
                PlayerDeadCheck();
                img[i].GetComponent<enemyCombatAnim>().Retract();
                yield return new WaitForSeconds(1f);
            }
        }

        Invoke("PlayerTurn", 1);
    }

    private void PlayerDeadCheck()
    {
        for (int i = 0; i < allyParty.Length; i++)
        {
            if (allyParty[i] != null && allyParty[i].currentHP <= 0 && !allyParty[i].isDead)
            {
                if (i == 0)
                {
                    print("p");
                    EndCombat(false);
                }
                else
                {
                    allyParty[i].isDead = true;
                    aDisplay[i].SetActive(false);
                }
            }
        }

        uh.UpdateEveryHUD();
    }

    //this is where we would put functionality for if a battle is won or lost (win animations/lose states etc.)
    private void EndCombat(bool won)
    {
        if(won)
        {
            List<string> s = new List<string>();
            for(int i = 1; i < allyParty.Length; i++)
            {
                if (allyParty[i] != null &&!allyParty[i].isDead) s.Add(allyParty[i].eName);
            }

            ListCreator.combatMinionsList = s;

            Debug.Log("You did it!");

            if(!debugSession)
            {
                print(1);
                ActiveOverworldEntity.entityInDimension[1][0][id] = false;
                ActiveOverworldEntity.entityCount[1]--;
            }
            else ActiveOverworldEntity.dim = 1;

            LeaveBattle();
        }
        else
        {
            Debug.Log("You died");
            player1.currentHP = player1.maxHP;

            LeaveBattle();
        }
    }

    public void LeaveBattle()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    //Displays to player what the enemy did for its attack
    private Sprite ImageAssign(string s)
    {
        switch(s)
        {
            case "Heal":
            {
                return actions[0];
            }
            case "Attack":
            {
                return actions[1];
            }
            case "Buff":
            {
                return actions[2];
            }
            case "Debuff":
            {
                return actions[3];
            }
            default:
            {
                return null;
            }
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatSystem : MonoBehaviour
{
    //Debug functionality
    public Enemy[] dE;

    //Combative parties
    public Player player1; 

    public static Enemy[] enemyParty; 
    public static Entity[] allyParty = new Entity[4];

    //Entity placements
    public Transform playerSpawn; 
    public Transform[] pos; //Spawn points for enemies

    //Party UI

    public Image[] eDisplay; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public Image[] aDisplay; //The panel that ally info is listed on. [Disable to make everything disabled.]
    public Image[] enemyAction;
    public Sprite[] actions;

    public static int livingEnemies;

    //Accessed classes
    private EnemyMoves em;
    private PlayerMoves pm;
    private UpdateHUD uh;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyParty == null)
        {
            enemyParty = new Enemy[dE.Length];

            for(int i = 0; i < dE.Length; i++) enemyParty[i] = Instantiate(dE[i]);
        }

        livingEnemies = enemyParty.Length;
        
        em = FindObjectOfType<EnemyMoves>();
        pm = FindObjectOfType<PlayerMoves>();
        uh = FindObjectOfType<UpdateHUD>();

        SetUpCombat();
    }

    private void SetUpCombat()
    {
        allyParty[0] = player1;

        uh.LoadHUDs();
        uh.UpdateEveryHUD();

        PlayerTurn();
    }


    /*
        Combat cycle is: Player turn, Minion turn, Enemy turn. Check if attacked party has dead members 
        after each opposing party member attacks.
    */


    private void PlayerTurn()
    {
        pm.PlayerDecision(player1, enemyParty);
    }

    public void EnemyDeadCheck()
    {
        for (int i = 0; i < enemyParty.Length; i++)
            if (enemyParty[i].currentHP <= 0 && !enemyParty[i].isDead)
            {
                enemyParty[i].isDead = true;
                eDisplay[i].gameObject.SetActive(false);
                livingEnemies--;
            }

        uh.UpdateEveryHUD();

        if (livingEnemies <= 0)
        {
            Debug.Log("Battle Won");
            EndCombat(true);
        }
    }

    //this could later be used to decide what attacks the enemy is doing
    public void EnemyTurn()
    {
        for(int i = 0; i < enemyParty.Length; i++)
            //Enemy move is decided if enemy is alive
            if(!enemyParty[i].isDead) enemyAction[i].sprite = ImageAssign(em.ChooseAction(enemyParty[i]));
        PlayerDeadCheck();
    }

    private void PlayerDeadCheck()
    {
        for (int i = 0; i < allyParty.Length; i++)
        {
            if (allyParty[i] != null && allyParty[i].currentHP <= 0 && !allyParty[i].isDead)
            {
                if (i == 0) EndCombat(false);
                else
                {
                    allyParty[i].isDead = true;
                    aDisplay[i].gameObject.SetActive(false);
                }
            }
        }

        uh.UpdateEveryHUD();

        Invoke("PlayerTurn", 1);
    }

    //this is where we would put functionality for if a battle is won or lost (win animations/lose states etc.)
    private void EndCombat(bool won)
    {
        if(won)
        {
            Debug.Log("You did it!");
            SceneManager.LoadScene(3);
        }
        else
        {
            Debug.Log("You died");
            SceneManager.LoadScene(2);
        }
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
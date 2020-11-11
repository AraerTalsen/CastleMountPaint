using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    //Paty UI
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerHPText;
    public Slider playerHPSlider;

    public Image[] display; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public TextMeshProUGUI[] enemyHP;
    public TextMeshProUGUI[] enemyName;
    public Image[] enemyAction;
    public Sprite[] actions;
    public Slider[] enemyHPSlider;
    public Image[] enemyImg;

    public static int livingEnemies;

    //Accessed libraries
    private EnemyMoves em;
    private PlayerMoves pm;
    private UpdateHUD uh;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyParty == null) enemyParty = dE;

        livingEnemies = enemyParty.Length;
        
        em = FindObjectOfType<EnemyMoves>();
        pm = FindObjectOfType<PlayerMoves>();
        uh = FindObjectOfType<UpdateHUD>();

        SetUpCombat();
    }

    private void SetUpCombat()
    {
        //Player UI setup
        Instantiate(player1.playerPrefab, playerSpawn);
        player1.currentHP = player1.maxHP;
        player1.HitValue = player1.baseHitValue;
        playerNameText.text = "Name: " + player1.eName;
        playerHPSlider.maxValue = player1.maxHP;

        //Enemy UI setup
        for (int i = 0; i < enemyParty.Length; i++)
        {
            enemyParty[i] = Instantiate(enemyParty[i], pos[i]);

            display[i].gameObject.SetActive(true);

            enemyParty[i].currentHP = enemyParty[i].maxHP;
            enemyParty[i].HitValue = enemyParty[i].baseHitValue;

            enemyName[i].text = "Name: " + enemyParty[i].eName;
            enemyHPSlider[i].maxValue = enemyParty[i].maxHP;

            enemyImg[i].sprite = enemyParty[i].enemySprite;
        }

        allyParty[0] = player1;

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
            if (enemyParty[i].currentHP <= 0)
            {
                enemyParty[i].isDead = true;
                display[i].gameObject.SetActive(false);
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
            if(!enemyParty[i].isDead) enemyAction[i].sprite = ImageAssign(em.ChooseAction(player1, enemyParty[i]));
        PlayerDeadCheck();
    }

    private void PlayerDeadCheck()
    {
        uh.UpdateEveryHUD();

        if (player1.currentHP <= 0)
        {
            EndCombat(false);
        }
        else
        {
            //if the player is not dead start the cycle over(Player Turn)
            Invoke("PlayerTurn", 1);
        }
    }

    //this is where we would put functionality for if a battle is won or lost (win animations/lose states etc.)
    void EndCombat(bool won)
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
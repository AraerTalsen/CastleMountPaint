using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, MINIONTURN, WON, LOST} //sets up state machine for combat

public class CombatSystem : MonoBehaviour
{
    //States
    public BattleState state;

    //SetUpCombat
    public Player player1; //player scriptable object reference

    public Player summonedMinion1, summonedMinion2, summonedMinion3; //player summonable reference

    private Enemy[] enemyParty; //enemy scriptable object references

    public Transform playerSpawn; //Spawn point for player character
    public Transform minionSpawn1, minionSpawn2, minionSpawn3;
    public Transform[] pos; //Spawn points for enemies

    public TextMeshProUGUI playerNameText; //player UI
    public TextMeshProUGUI playerHPText;
    public Slider playerHPSlider;

    public TextMeshProUGUI minionNameText1, minionNameText2, minionNameText3; //minion UI
    public TextMeshProUGUI minionHPText1, minionHPText2, minionHPText3;
    public Slider minionHPSlider1, minionHPSlider2, minionHPSlider3;

    public TextMeshProUGUI[] enemyHP;
    public TextMeshProUGUI[] enemyName;
    public Image[] enemyAction;
    public Sprite[] actions;
    public Slider[] enemyHPSlider;
    public Image[] enemyImg;

    public bool enemySelected = false;

    public static int livingEnemies;
    private EnemyMoves em;
    private PlayerMoves pm;

    public bool playerDeadCheckBool = false;

    public int numMinionsSummoned = 0;

    public GameObject minionAttackButton1, minionAttackButton2, minionAttackButton3;
    public GameObject minionHealButton1, minionHealButton2, minionHealButton3;

    public static bool enemy1Dead = false, enemy2Dead = false, enemy3Dead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyParty = BattleHandler.EnemyParty();
        livingEnemies = enemyParty.Length;

        em = FindObjectOfType<EnemyMoves>();
        pm = FindObjectOfType<PlayerMoves>();

        state = BattleState.START; //sets the state to START
        StartCoroutine(SetUpCombat()); //calls SetUpCombat Coroutine
        enemy1Dead = false;
        enemy2Dead = false;
        enemy3Dead = false;
    }

    IEnumerator SetUpCombat()
    {
        //Debug.Log("Set up Combat");

        ///Set Up Combat Scene///
        Instantiate(player1.playerPrefab, playerSpawn);
        //resets player HP, this will change for the real game since we want the value to stay consistant between battles
        player1.currentHP = player1.maxHP;
        //resets the player attack value to baseline, removes debuffs and buffs
        //can also be used later to apply permanent buffs to player (picking up item can increase baseline permanently)
        player1.HitValue = player1.baseHitValue;
        //Sets up UI for Player
        playerNameText.text = "Name: " + player1.eName;
        playerHPText.text = "HP: " + player1.maxHP;
        playerHPSlider.maxValue = player1.maxHP;
        //sets Minion name
        minionNameText1.text = "Name: " + summonedMinion1.eName;
        minionNameText2.text = "Name: " + summonedMinion2.eName;
        minionNameText3.text = "Name: " + summonedMinion3.eName;
        MinionBehaviours.numMinionsSendable = 0;

        for (int i = 0; i < enemyParty.Length; i++)
        {
            enemyParty[i] = Instantiate(enemyParty[i], pos[i]);
            enemyParty[i].currentHP = enemyParty[i].maxHP;
            enemyParty[i].HitValue = enemyParty[i].baseHitValue;
            enemyParty[i].targeted = false;
            enemyParty[i].isDead = false;
            enemyParty[i].personality = EnemyPersonality.RandomAssignPersonality();
            enemyParty[i].pWeightRange = EnemyPersonality.AssignPersonalityWeights(enemyParty[i].personality);

            enemyHP[i].enabled = true;
            enemyHP[i].text = "HP: " + enemyParty[i].maxHP;
            enemyName[i].enabled = true;
            enemyName[i].text = "Name: " + enemyParty[i].eName;
            enemyHPSlider[i].enabled = true;
            enemyHPSlider[i].maxValue = enemyParty[i].maxHP;
            enemyImg[i].sprite = enemyParty[i].enemySprite;
        }

        yield return new WaitForSeconds(2f);

        PlayerTurn(); //switches to Player's Decision
    }

    private void PlayerTurn()
    {
        state = BattleState.PLAYERTURN; //changes the state to Player's Turn

        if (enemy1Dead && enemy2Dead && enemy3Dead)
        {
            EnemyDeadCheck();
        }

        pm.PlayerDecision(player1, enemyParty);
        Debug.Log("player turn started");


    }

    public void MinionTurn()
    {
        Debug.Log("MinionTurnStarted");
        if (MinionBehaviours.numMinionsSendable > 0)
        {
            state = BattleState.MINIONTURN; //changes the state to Minion's Turn
        } else {
            EnemyDeadCheck();
        }

    }

    public void MinionAttack()
    {
        enemyParty[Random.Range(0, 2)].currentHP = enemyParty[Random.Range(0, 2)].currentHP - 1;
        EnemyDeadCheck();
    }

    public void MinionHeal()
    {
        player1.currentHP = player1.currentHP + 1;
        EnemyDeadCheck();
    }

    //Checks if any enemies are dead after the Player Attacks
    public void EnemyDeadCheck()
    {
        Debug.Log("EnemyDeadCheck Function Running");
        if (livingEnemies <= 0) //if all enemies are dead
        {
            //Battle Won
            Debug.Log("Battle Won");
            state = BattleState.WON; //switch the state to Won
            EndCombat(); //start End Combat Function
        }
        else
        {
            //Enemy Turn
            player1.targeted = true; //the player is now targeted
            state = BattleState.ENEMYTURN; //the state is now the Enemy Turn
            Invoke("EnemyTurn", 1); //switch to the Enemy Turn Function with a small delay
        }
    }

    //checks if it is the enemy's turn
    //this could later be used to decide what attacks the enemy is doing
    void EnemyTurn()
    {
        if (state != BattleState.ENEMYTURN) //if the state is not Enemy Turn it will not work
            return;
        //Debug.Log("Start Enemy Turn");
        for(int i = 0; i < enemyParty.Length; i++)
            if(!enemyParty[i].isDead) enemyAction[i].sprite = ImageAssign(em.ChooseAction(player1, enemyParty[i], enemyParty));
        PlayerDeadCheck(); //switches to Player Dead Check
    }

    //Checks to see if the Player is dead
    void PlayerDeadCheck()
    {
        if (state != BattleState.ENEMYTURN) //if it is not the enemy turn it will not check
            return;
        //Debug.Log("Check if Player is Dead");
        if (player1.currentHP <= 0) //if the player's health value is less than or equal to 0
        {
            state = BattleState.LOST; //switch state to Lost
            EndCombat(); //Start End Combat function
        }
        else
        {
            //pm.PlayerDecision(player1, enemyParty); //if the player is not dead start the cycle over(Player Turn)
            Invoke("PlayerTurn", 1);
        }
    }

    //checks which state is active when entering the function
    //this is where we would put functionality for if a battle is won or lost (win animations/lose states etc.)
    void EndCombat()
    {
        if(state == BattleState.WON) //if the state is Won
        {
            Debug.Log("You did it!");
            SceneManager.LoadScene(3);
        }
        if (state == BattleState.LOST) //if the state is Lost
        {
            Debug.Log("You died");
            SceneManager.LoadScene(2);
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "HP: " + player1.currentHP;
        playerHPSlider.value = player1.currentHP;

        minionHPText1.text = "HP: " + summonedMinion1.currentHP; //SummonedMinion Text Update
        minionHPSlider1.value = summonedMinion1.currentHP;

        minionHPText2.text = "HP: " + summonedMinion2.currentHP; //SummonedMinion Text Update
        minionHPSlider2.value = summonedMinion2.currentHP;

        minionHPText3.text = "HP: " + summonedMinion3.currentHP; //SummonedMinion Text Update
        minionHPSlider3.value = summonedMinion3.currentHP;

        for (int i = 0; i < enemyParty.Length; i++)
        {
            enemyHP[i].text = "HP: " + enemyParty[i].currentHP;
            enemyHPSlider[i].value = enemyParty[i].currentHP;
        }

        //For some reason, this needs to be here. 
        //In the player dead check function, it skips the first check. If you play through the next round, the player will die at the start of the player dead check. 
        //This is most likely temp fix, but for now it solves the problem
        if (player1.currentHP <= 0) //if the player's health value is less than or equal to 0
        {
            state = BattleState.LOST; //switch state to Lost
            EndCombat(); //Start End Combat function
        }

        //enables and disables the minion attack and heal buttons so that it is easy to see what turn it is
        if (state == BattleState.MINIONTURN)
        {
            minionAttackButton1.SetActive(true);
            minionAttackButton2.SetActive(true);
            minionAttackButton3.SetActive(true);
            minionHealButton1.SetActive(true);
            minionHealButton2.SetActive(true);
            minionHealButton3.SetActive(true);
        }
        else
        {
            minionAttackButton1.SetActive(false);
            minionAttackButton2.SetActive(false);
            minionAttackButton3.SetActive(false);
            minionHealButton1.SetActive(false);
            minionHealButton2.SetActive(false);
            minionHealButton3.SetActive(false);
        }

        //janky garbage but I dont know what is causing them to not die, this is temp fix i think
        if (enemyParty[0].currentHP <= 0)
        {
            enemyParty[0].isDead = true;
            enemy1Dead = true;
        }
        if (enemyParty[1].currentHP <= 0)
        {
            enemyParty[1].isDead = true;
            enemy2Dead = true;
        }
        if (enemyParty[2].currentHP <= 0)
        {
            enemyParty[2].isDead = true;
            enemy3Dead = true;
        }

        if (enemy1Dead && enemy2Dead && enemy3Dead)
        {
            livingEnemies = 0;
        }

    }
}
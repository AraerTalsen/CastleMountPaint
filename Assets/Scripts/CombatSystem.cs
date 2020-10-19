﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST} //sets up state machine for combat

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

    //Player Turn
    public GameObject attackButton;
    public GameObject summonAllyButton;
    public GameObject healAlliesButton;
    public GameObject sketchEnemiesButton;

    //Select Target
    public Button[] enemySelect;

    public GameObject minionHUD1, minionHUD2, minionHUD3;

    public bool minionSummoned1 = false;
    public bool minionSummoned2 = false;
    public bool minionSummoned3 = false;

    public bool enemySelected = false;

    private int livingEnemies;
    private EnemyMoves em;

    public bool playerDeadCheckBool = false;

    public int numMinionsSummoned = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyParty = BattleHandler.EnemyParty();
        livingEnemies = enemyParty.Length;

        em = FindObjectOfType<EnemyMoves>();

        state = BattleState.START; //sets the state to START
        StartCoroutine(SetUpCombat()); //calls SetUpCombat Coroutine
    }

    IEnumerator SetUpCombat()
    {
        Debug.Log("Set up Combat");

        ///Set Up Combat Scene///
        Instantiate(player1.playerPrefab, playerSpawn);
        //resets player HP, this will change for the real game since we want the value to stay consistant between battles
        player1.currentHP = player1.maxHP;
        //resets the player attack value to baseline, removes debuffs and buffs
        //can also be used later to apply permanent buffs to player (picking up item can increase baseline permanently)
        player1.HitValue = player1.baseHitValue;
        //Sets up UI for Player
        playerNameText.text = "Name: " + player1.playerName;
        playerHPText.text = "HP: " + player1.maxHP;
        playerHPSlider.maxValue = player1.maxHP;
        //sets Minion name
        minionNameText1.text = "Name: " + summonedMinion1.playerName;
        minionNameText2.text = "Name: " + summonedMinion2.playerName;
        minionNameText3.text = "Name: " + summonedMinion3.playerName;

        for (int i = 0; i < enemyParty.Length; i++)
        {
            print(enemyParty[i].enemyName);
            enemyParty[i] = Instantiate(enemyParty[i], pos[i]);
            enemyParty[i].currentHP = enemyParty[i].maxHP;
            enemyParty[i].HitValues = enemyParty[i].baseHitValue;
            enemyParty[i].enemyTargeted = false;
            enemyParty[i].isDead = false;
            enemyParty[i].personality = EnemyPersonality.RandomAssignPersonality();
            enemyParty[i].pWeightRange = EnemyPersonality.AssignPersonalityWeights(enemyParty[i].personality);
            print(enemyParty[i].personality);

            enemyHP[i].enabled = true;
            enemyHP[i].text = "HP: " + enemyParty[i].maxHP;
            enemyName[i].enabled = true;
            enemyName[i].text = "Name: " + enemyParty[i].enemyName;
            enemyHPSlider[i].enabled = true;
            enemyHPSlider[i].maxValue = enemyParty[i].maxHP;
            enemyImg[i].sprite = enemyParty[i].enemySprite;
        }

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN; //changes the state to Player's Turn
        StartCoroutine(PlayerDecision()); //switches to Player's Decision
    }

    //Player Decision UI elements pop up
    IEnumerator PlayerDecision()
    {
        Debug.Log("Start Player Turn");
        state = BattleState.PLAYERTURN;

        yield return new WaitForSeconds(2f);

        //this is where we could show player options (Basic Attacks, Special Attacks, Items etc.)
        if (minionSummoned1 == true && minionSummoned2 == true && minionSummoned3 == true)
        {
            summonAllyButton.SetActive(false);
        } else
        {
            summonAllyButton.SetActive(true);
        }
        attackButton.SetActive(true);
        healAlliesButton.SetActive(true);
        sketchEnemiesButton.SetActive(true);
    }

    //Attack Button Functionality
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not work
            return;
        attackButton.SetActive(false);
        summonAllyButton.SetActive(false);
        healAlliesButton.SetActive(false);
        sketchEnemiesButton.SetActive(false);

        SelectTarget(); //switch to the Choose Target Function
    }

    //Summon Button Functionality
    public void OnSummonButton()
    {
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not work
            return;
        attackButton.SetActive(false); //once it is pressed, the button is hidden
        summonAllyButton.SetActive(false);
        healAlliesButton.SetActive(false);
        sketchEnemiesButton.SetActive(false);

        //needs to create new set of functions for summoning (do we want multiple summon types, how many?)
        StartCoroutine(SummonPlayerAlly()); //switch to the summon ally function
    }

    //Player Escape Combat Button Functionality
    //This is being left as empty for now, overworld is needed for combat to be escaped from
    public void OnSketchEnemiesButton()
    {
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not work
            return;
        attackButton.SetActive(false); //once it is pressed, the button is hidden
        summonAllyButton.SetActive(false);
        healAlliesButton.SetActive(false);
        sketchEnemiesButton.SetActive(false);

        //Call coroutine to escape comabt
        StartCoroutine(SketchEnemies());

    }

    //Player Healing Button Functionality
    public void OnHealAlliesButton()
    {
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not work
            return;
        attackButton.SetActive(false); //once it is pressed, the button is hidden
        summonAllyButton.SetActive(false);
        healAlliesButton.SetActive(false);
        sketchEnemiesButton.SetActive(false); ;

        StartCoroutine(HealPlayerAllies());//start the heal allies coroutine

    }

    //Select an Enemy to Target
    void SelectTarget()
    {
        Debug.Log("Select an Enemy");

        for(int i = 0; i < enemyParty.Length; i++)
        {
            if (!enemyParty[i].isDead) enemySelect[i].gameObject.SetActive(true);
        }
    }

    public void EnemySelectButton()
    {
        for (int i = 0; i < enemySelect.Length; i++) enemySelect[i].gameObject.SetActive(false); //hide all the buttons 

        int select;
        int.TryParse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name, out select);
        Debug.Log("Enemy " + select +" Targeted");
        enemyParty[select].enemyTargeted = true; //Target Toggle on Enemy Object is set to true
        StartCoroutine(PlayerAttack()); //switch to Player Attack
    }

    //Used to delay the Player's Attack
    IEnumerator PlayerAttack()
    {
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
        yield return new WaitForSeconds(2f);
        HealAllies(); //sketch the enemies
    }

    //Used to delay the Player's Attack
    IEnumerator SummonPlayerAlly()
    {
        yield return new WaitForSeconds(2f);
        //Summon Ally for player
        SummonAllies(); //switches to the Damage Enemy function
    }

    //Checks which enemy was targeted and deals the correct damage
    void DamageEnemy()
    {
        for(int i = 0; i < enemyParty.Length; i++)
        {
            if (enemyParty[i].enemyTargeted == true) //if the enemy is targeted
            {
                Debug.Log("Enemy " + i + " has been Damaged");

                enemyParty[i].currentHP -= player1.HitValue; //apply the player's hit value
                Mathf.Clamp(enemyParty[i].currentHP, 0, enemyParty[i].maxHP);
                enemyParty[i].enemyTargeted = false; //Target Toggle switched to false
            }
            if (!enemyParty[i].isDead && enemyParty[i].currentHP <= 0) //if the enemy's health is less than or equal to 0
            {
                //Kill enemy
                Debug.Log("Enemy " + i + " is Dead");
                enemyParty[i].isDead = true; //isDead toogle on Enemy Object is toggled on
                livingEnemies--;
            }
        }
        EnemyDeadCheck();
    }

    void HealAllies()
    {
        player1.currentHP += player1.HitValue;
        if (player1.currentHP >= player1.maxHP)
        {
            player1.currentHP = player1.maxHP;
        }
        EnemyDeadCheck();
    }

    void SummonAllies()
    {
        if (numMinionsSummoned == 0)
        {
            numMinionsSummoned++;
            minionSummoned1 = true;
            if (minionSummoned1 == true)
            {
                minionHUD1.SetActive(true);
                Instantiate(summonedMinion1.playerPrefab, minionSpawn1);
            }
            EnemyDeadCheck();
        } else if (numMinionsSummoned == 1)
        {
            numMinionsSummoned++;
            minionSummoned2 = true;
            if (minionSummoned2 == true)
            {
                minionHUD2.SetActive(true);
                Instantiate(summonedMinion2.playerPrefab, minionSpawn2);
            }
            EnemyDeadCheck();
        } else if (numMinionsSummoned == 2)
        {
            minionSummoned3 = true;
            if (minionSummoned3 == true)
            {
                minionHUD3.SetActive(true);
                Instantiate(summonedMinion3.playerPrefab, minionSpawn3);
            }
            EnemyDeadCheck();
        } else {
            EnemyDeadCheck();
        }
        Debug.Log("Summoned a minion");
    }

    //Checks if any enemies are dead after the Player Attacks
    void EnemyDeadCheck()
    {
        /*Debug.Log("Check if Enemy is dead");
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not check
            return;

        for(int i = 0; i < enemyParty.Length; i++)
        {
            if (enemyParty[i].currentHP <= 0) //if the enemy's health is less than or equal to 0
            {
                //Kill enemy
                Debug.Log("Enemy " + i + " is Dead");
                enemyParty[i].isDead = true; //isDead toogle on Enemy Object is toggled on
                livingEnemies--;
            }
        }*/

        if(livingEnemies <= 0) //if all enemies are dead
        {
            //Battle Won
            Debug.Log("Battle Won");
            state = BattleState.WON; //switch the state to Won
            EndCombat(); //start End Combat Function
        }
        else
        {
            //Enemy Turn
            player1.playerTargeted = true; //the player is now targeted
            state = BattleState.ENEMYTURN; //the state is now the Enemy Turn
            EnemyTurn(); //switch to the Enemy Turn Function
        }
    }

    //checks if it is the enemy's turn
    //this could later be used to decide what attacks the enemy is doing
    void EnemyTurn()
    {
        if (state != BattleState.ENEMYTURN) //if the state is not Enemy Turn it will not work
            return;
        Debug.Log("Start Enemy Turn");
        for(int i = 0; i < enemyParty.Length; i++)
            if(!enemyParty[i].isDead) enemyAction[i].sprite = ImageAssign(em.ChooseAction(player1, enemyParty[i], enemyParty));
        PlayerDeadCheck(); //switches to Player Dead Check
    }

    //Checks to see if the Player is dead
    void PlayerDeadCheck()
    {
        if (state != BattleState.ENEMYTURN) //if it is not the enemy turn it will not check
            return;
        Debug.Log("Check if Player is Dead");
        if (player1.currentHP <= 0) //if the player's health value is less than or equal to 0
        {
            state = BattleState.LOST; //switch state to Lost
            EndCombat(); //Start End Combat function
        }
        else
        {
            StartCoroutine(PlayerDecision()); //if the player is not dead start the cycle over(Player Turn)
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

    private void StateChange()
    {
        //Player turn
        StartCoroutine(PlayerDecision());

        if(livingEnemies > 0)
        {
            player1.playerTargeted = true;//the player is now targeted
            EnemyTurn(); //switch to the Enemy Turn Function
        }
        //over?

        //Enemy turn
        if(player1.currentHP > 0)
        {

        }
        //over?
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
    }
}
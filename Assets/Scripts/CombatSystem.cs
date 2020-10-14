using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST} //sets up state machine for combat

public class CombatSystem : MonoBehaviour
{
    //States
    public BattleState state;

    //SetUpCombat
    public Player player1; //player scriptable object reference

    private Enemy[] enemyParty; //enemy scriptable object references
    private Enemy[] e;

    public Transform playerSpawn; //Spawn point for player character
    public Transform[] pos; //Spawn points for enemies

    public TextMeshProUGUI playerNameText; //player UI
    public TextMeshProUGUI playerHPText;
    public Slider playerHPSlider;

    public TextMeshProUGUI[] enemyHP;
    public TextMeshProUGUI[] enemyName;
    public TextMeshProUGUI[] enemyAction;
    public Slider[] enemyHPSlider;
    public Image[] enemyImg;

    //Player Turn
    public GameObject attackButton;

    //Select Target
    public Button[] enemySelect;

    public bool enemySelected = false;

    private int livingEnemies;
    private EnemyMoves em;

    // Start is called before the first frame update
    void Start()
    {
        e = BattleHandler.EnemyParty();
        enemyParty = new Enemy[e.Length];
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

        for (int i = 0; i < e.Length; i++)
        {
            print(e[i].enemyName);
            enemyParty[i] = Instantiate(e[i], pos[i]);
            enemyParty[i].currentHP = enemyParty[i].maxHP;
            enemyParty[i].HitValues = enemyParty[i].baseHitValue;
            enemyParty[i].enemyTargeted = false;
            enemyParty[i].isDead = false;

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
        attackButton.SetActive(true);
    }

    //Attack Button Functionality
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) //if the state is not Player Turn it will not work
            return;
        attackButton.SetActive(false); //once it is pressed, the button is hidden

        SelectTarget(); //switch to the Choose Target Function
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

    //Checks which enemy was targeted and deals the correct damage
    void DamageEnemy()
    {
        for(int i = 0; i < enemyParty.Length; i++)
        {
            if (enemyParty[i].enemyTargeted == true) //if the enemy is targeted
            {
                Debug.Log("Enemy " + i + " has been Damaged");

                enemyParty[i].currentHP = Mathf.Clamp(player1.HitValue, 0, enemyParty[i].maxHP); //apply the player's hit value
                enemyParty[i].enemyTargeted = false; //Target Toggle switched to false
            }
            if (enemyParty[i].currentHP <= 0) //if the enemy's health is less than or equal to 0
            {
                //Kill enemy
                Debug.Log("Enemy " + i + " is Dead");
                enemyParty[i].isDead = true; //isDead toogle on Enemy Object is toggled on
                livingEnemies--;
            }
        }
        EnemyDeadCheck();
    }

    //Checks if any enemies are dead after the Player Attacks
    void EnemyDeadCheck()
    {
        Debug.Log("Check if Enemy is dead");
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
        }

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
        for(int i = 0; i < enemyParty.Length; i++) enemyAction[i].text = em.ChooseAction(player1, enemyParty[i], enemyParty);
        PlayerDeadCheck(); //switches to Player Dead Check
    }

    //Checks to see if the Player is dead
    void PlayerDeadCheck()
    {
        if (state != BattleState.ENEMYTURN) //if it is not the enemy turn it will not check
            return;
        Debug.Log("Check if Player is Dead");
        if (player1.currentHP <= 0) //if the player's health calue is less than or equal to 0
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
            //change scene
        }
        if (state == BattleState.LOST) //if the state is Lost
        {
            Debug.Log("You died");
            //change scene
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

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemyParty.Length; i++)
        {
            playerHPText.text = "HP: " + player1.currentHP;
            playerHPSlider.value = player1.currentHP;

            enemyHP[i].text = "HP: " + enemyParty[i].currentHP;
            enemyHPSlider[i].value = enemyParty[i].currentHP;
        }
    }
}
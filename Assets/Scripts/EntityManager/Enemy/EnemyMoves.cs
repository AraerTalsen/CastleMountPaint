using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : EntityBehaviours
{
    public MoveChoice[] moves;
    private MoveChoice selectedMove;

    public delegate void MoveChoice(Entity target, Entity user);

    private void Start()
    {
        moves = new MoveChoice[] { DealDamage, HealAllies, BuffAlly, DebuffOpponent };
    }

    private void Directory(int index)
    {
        selectedMove = moves[index];
    }

    public void UseMove(int index, Entity target, Entity user)
    {
        Directory(index);

        selectedMove(target, user);
    }

    //checks if it is the enemy's turn
    //this could later be used to decide what attacks the enemy is doing
    public string ChooseAction(Entity t, Enemy e, Enemy[] enemies)
    {
        //random choose an attack state for the enemy to perform
        int enemyAttackPhase = Random.Range(0, 100);
        /*
        if (enemyAttackPhase <= e.pWeightRange[0])
        {
            StartCoroutine(BuffFellowEnemies(t, enemies)); //Start Buff Allies
            return "Buff";
        }
        else if (enemyAttackPhase > e.pWeightRange[0] && enemyAttackPhase <= e.pWeightRange[1])
        {
            HealAllies(t, e); //Start Heal Allies
            return "Heal";
        }
        else if (enemyAttackPhase > e.pWeightRange[1] && enemyAttackPhase <= e.pWeightRange[2])
        {
            StartCoroutine(DebuffPlayer(t)); //Start Debuff Player
            return "Debuff";
        }
        else if (enemyAttackPhase > e.pWeightRange[2])
        {
            DealDamage(t, e); //Start Take Damage
            return "Attack";
        }
        else
        {
            DealDamage(t, e); //Start Take Damage
            return "Attack";
        }
        */

        //This is a temp workaround until we get the personalites sorted. At the moment, they super heavily favor buffing themselves, this should smooth that out for playtest
        if (enemyAttackPhase <= 24)
        {
            BuffAlly(t, e); //Start Buff Allies
            return "Buff";
        }
        else if (enemyAttackPhase > 24 && enemyAttackPhase <= 49)
        {
            HealAllies(t, e); //Start Heal Allies
            return "Heal";
        }
        else if (enemyAttackPhase > 49 && enemyAttackPhase <= 74)
        {
            DebuffOpponent(t, e); //Start Debuff Player
            return "Debuff";
        }
        else if (enemyAttackPhase > 74)
        {
            DealDamage(t, e); //Start Take Damage
            return "Attack";
        }
        else
        {
            DealDamage(t, e); //Start Take Damage
            return "Attack";
        }
    }

    //Applies damage onto the player
    /*IEnumerator DealDamage(Entity t, Enemy e)
    {
        if (t.targeted == true) //if the player is targeted
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Damage to Player");

            t.currentHP -= e.HitValue; //apply enemy's hit value to the player
            t.targeted = false; //the player is no longer targeted
        }
    }*/

    //Applies damage onto the player
    private void BuffAlly(Entity target, Entity user)
    {
        //Debug.Log("Increase enemy ally damage");
        //increase the amount of damage that the enemies are dealing 

        if (!target.isDead) target.HitValue++;

        target.targeted = false; //the player is no longer targeted
    }

    //Applies debuff to the player
    private void DebuffOpponent(Entity target, Entity user)
    {
        if (target.targeted == true) //if the player is targeted
        {
            print("Debuff player");

            target.HitValue = target.HitValue--; //apply enemy's hit value to the player
            target.targeted = false; //the player is no longer targeted
        }
    }

    //Enemies heal one another using this
    /*IEnumerator HealFellowEnemies(Entity t, Enemy[]enemies)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Healing for other enemies");

        for (int i = 0; i < enemies.Length; i++)
        {
            if(!enemies[i].isDead)
            {
                enemies[i].currentHP += enemies[i].HitValue;
                enemies[i].currentHP = Mathf.Clamp(enemies[i].currentHP, 0, enemies[i].maxHP);
            }
        }

        t.targeted = false; //the player is no longer targeted
    }*/
}

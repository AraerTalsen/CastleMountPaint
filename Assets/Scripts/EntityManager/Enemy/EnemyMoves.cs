using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : EntityBehaviours
{
    //checks if it is the enemy's turn
    //this could later be used to decide what attacks the enemy is doing
    public string ChooseAction(Entity t, Enemy e, Enemy[] enemies)
    {
        //random choose an attack state for the enemy to perform
        int enemyAttackPhase = Random.Range(0, 100);

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
    IEnumerator BuffFellowEnemies(Entity t, Enemy[]enemies)
    {
        yield return new WaitForSeconds(2f);
        //Debug.Log("Increase enemy ally damage");
        //increase the amount of damage that the enemies are dealing 
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].isDead) enemies[i].HitValue++;
        }
        t.targeted = false; //the player is no longer targeted
    }

    //Applies debuff to the player
    IEnumerator DebuffPlayer(Entity t)
    {
        if (t.targeted == true) //if the player is targeted
        {
            yield return new WaitForSeconds(2f);
            print("Debuff player");

            t.HitValue = t.HitValue--; //apply enemy's hit value to the player
            t.targeted = false; //the player is no longer targeted
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

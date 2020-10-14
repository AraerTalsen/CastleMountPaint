using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    //checks if it is the enemy's turn
    //this could later be used to decide what attacks the enemy is doing
    public string ChooseAction(Player p, Enemy e, Enemy[] enemies)
    {
        //random choose an attack state for the enemy to perform
        int enemyAttackPhase = Random.Range(0, 3);

        if (enemyAttackPhase == 0)
        {
            StartCoroutine(HealFellowEnemies(p, enemies)); //Start Heal Allies
            return "Heal";
        }
        else if (enemyAttackPhase == 1)
        {
            StartCoroutine(DealDamage(p, e)); //Start Take Damage
            return "Attack";
        }
        else if (enemyAttackPhase == 2)
        {
            StartCoroutine(BuffFellowEnemies(p, enemies)); //Start Buff Allies
            return "Buff";
        }
        else if (enemyAttackPhase == 3)
        {
            StartCoroutine(DebuffPlayer(p)); //Start Debuff Player
            return "Debuff";
        }
        else
        {
            StartCoroutine(DealDamage(p, e)); //Start Take Damage
            return "Attack";
        }
    }

    //Applies damage onto the player
    IEnumerator DealDamage(Player p, Enemy e)
    {
        if (p.playerTargeted == true) //if the player is targeted
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Damage to Player");

            p.currentHP -= e.HitValues; //apply enemy's hit value to the player
            p.playerTargeted = false; //the player is no longer targeted
        }
    }

    //Applies damage onto the player
    IEnumerator BuffFellowEnemies(Player p, Enemy[]enemies)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Increase enemy ally damage");

        //increase the amount of damage that the enemies are dealing 
        for (int i = 0; i < enemies.Length; i++) enemies[i].HitValues++;
        p.playerTargeted = false; //the player is no longer targeted
    }

    //Applies debuff to the player
    IEnumerator DebuffPlayer(Player p)
    {
        if (p.playerTargeted == true) //if the player is targeted
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Damage to Player");

            p.HitValue = p.HitValue--; //apply enemy's hit value to the player
            p.playerTargeted = false; //the player is no longer targeted
        }
    }

    //Enemies heal one another using this
    IEnumerator HealFellowEnemies(Player p, Enemy[]enemies)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Healing for other enemies");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].currentHP = Mathf.Clamp(enemies[i].HitValues, 0, enemies[i].maxHP);
        }

        p.playerTargeted = false; //the player is no longer targeted
    }
}

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
        moves = new MoveChoice[] { DealDamage, HealAllies, DebuffOpponent, BuffAlly };
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
    public string ChooseAction(Entity t, Entity user)
    {
        Entity target;
        string action;
        //random choose an attack state for the enemy to perform
        int enemyAttackPhase = Random.Range(0, 4);

        if (user.currentHP <= user.maxHP / 2) enemyAttackPhase = Random.Range(0, 2);

        Directory(enemyAttackPhase);

        switch(enemyAttackPhase)
        {
            case 0:
            {
                target = t;//Start Attack opponent
                action = "Attack";
                break;
            }
            case 1:
            {
                target = user;//Start Heal opponent
                action = "Heal";
                break;
            }
            case 2:
            {
                target = t;//Start Debuff opponent
                action = "Debuff";
                break;
            }
            case 3:
            {
                target = user;//Start Attack opponent
                action = "Buff";
                break;
            }
            default:
            {
                target = t;//Start Attack opponent
                action = "Attack";
                break;
            }

        }

        selectedMove(target, user);

        return action;
    }

    //Applies damage buff onto allies
    private void BuffAlly(Entity target, Entity user)
    {
        //Debug.Log("Increase enemy ally damage");
        //increase the amount of damage that the enemies are dealing 
        print(target.eName);
        if (!target.isDead) user.HitValue++;

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
}

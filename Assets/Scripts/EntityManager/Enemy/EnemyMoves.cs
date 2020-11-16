using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actions an enemy can perform while in combat
public class EnemyMoves : EntityBehaviours
{
    public delegate void MoveChoice(Entity target, Entity user);
    public MoveChoice[] moves;
    private MoveChoice selectedMove;

    private void Start()
    {
        moves = new MoveChoice[] { DealDamage, HealAllies, DebuffOpponent, BuffAlly };
    }

    //Switch move that delegate will use
    private void Directory(int index)
    {
        selectedMove = moves[index];
    }

    public void UseMove(int index, Entity target, Entity user)
    {
        Directory(index);

        selectedMove(target, user);
    }

    private Entity ChooseTarget()
    {
        Entity[] a = CombatSystem.allyParty;
        int count = 0, j = 0;

        //if (a[1] != null && !a[1].isDead) return a[1];

        for (int i = 0; i < a.Length; i++)
            if (a[i] != null && !a[i].isDead) count++;

        int[] active = new int[count];

        for(int i = 0; i < a.Length; i++)
            if (a[i] != null && !a[i].isDead)
            {
                active[j] = i;
                j++;
            }

        return a[active[Random.Range(0, active.Length)]];
    }

    //DEBUG: Enemies cannot buff or debuff because enemyAttackPhase (below) can only be set to 0 or 1,
    //ignoring moves 2 and 3.
    //Randomly choose an action for enemy to perform
    public string ChooseAction(Entity user)
    {
        Entity t = ChooseTarget();
        Entity target;
        string action;

        int enemyAttackPhase = Random.Range(0, 2);

        if (user.currentHP <= user.maxHP / 2) enemyAttackPhase = Random.Range(0, 2);

        Directory(enemyAttackPhase);

        if (user.eName == "Sean")
        {
            switch (enemyAttackPhase)
            {
                case 0:
                    {
                        target = t;//Start Attack opponent
                        action = "Attack";
                        break;
                    }
                case 1:
                    {
                        target = t;//Start Attack opponent
                        action = "Debuff";
                        break;
                    }
                default:
                    {
                        target = t;//Start Attack opponent
                        action = "Debuff";
                        break;
                    }
            }
            selectedMove(target, user);

            return action;

        }

        if (user.eName == "Dan")
        {
            switch (enemyAttackPhase)
            {
                case 0:
                    {
                        target = t;//Start Attack opponent
                        action = "Attack";
                        break;
                    }
                case 1:
                    {
                        target = t;//Start Attack opponent
                        action = "Heal";
                        break;
                    }
                default:
                    {
                        target = t;//Start Attack opponent
                        action = "Heal";
                        break;
                    }
            }
            selectedMove(target, user);

            return action;

        }

        if (user.eName == "Mike")
        {
            switch (enemyAttackPhase)
            {
                case 0:
                    {
                        target = t;//Start Attack opponent
                        action = "Attack";
                        break;
                    }
                case 1:
                    {
                        target = t;//Start Attack opponent
                        action = "Buff";
                        break;
                    }
                default:
                    {
                        target = t;//Start Attack opponent
                        action = "Buff";
                        break;
                    }
            }
            selectedMove(target, user);

            return action;

        } 

        /*
        switch (enemyAttackPhase)
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
        */
        return null;
        
    }

    //Applies hit value buff onto allies
    private void BuffAlly(Entity target, Entity user)
    {
        if (!target.isDead) user.HitValue++;
    }

    //Applies hit value debuff to the opponent
    private void DebuffOpponent(Entity target, Entity user)
    {
        if (!target.isDead)  target.HitValue--;
    }
}

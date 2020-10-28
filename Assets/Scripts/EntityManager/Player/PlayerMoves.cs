using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : EntityBehaviours
{
    
    
    //Select Target
    public int numMinions = 0;
    public MoveChoice[] moves;
    private Player p;
    private Enemy[] e;
    private Entity[] a = new Entity[5];
    private CombatSystem cs;
    private EnemyMoves em;
    private PlayerButtons pb;
    private MinionBehaviours mb;
    private MoveChoice selectedMove;
        
    public delegate void MoveChoice(Entity target, Entity user);

    public void Start()
    {
        pb = FindObjectOfType<PlayerButtons>();
        cs = FindObjectOfType<CombatSystem>();
        em = FindObjectOfType<EnemyMoves>();
        mb = FindObjectOfType<MinionBehaviours>();

        moves = new MoveChoice[4] { DealDamage, HealAllies, Sketch, SummonAllies };
    }

    //Player Decision UI elements pop up
    public void PlayerDecision(Player player, Enemy[] enemies)
    {
        //Debug.Log("Start Player Turn");

        p = player;
        pb.SetPlayer(p);
        e = enemies;
        a[0] = p;

        pb.PlayerNewTurn(0, e, a);
    }

    private void Directory(int index)
    {
        selectedMove = moves[index];
    }

    public void UseMove(int index, Entity target, Entity user)
    {
        Directory(index);

        selectedMove(target, user);

        cs.EnemyDeadCheck();

        mb.MinionTurn(1, e, a);

        //cs.EnemyDeadCheck();
    }

    //Used to delay the Player's Attack
    /*IEnumerator PlayerAttack()
    {
        print("Attack");
        yield return new WaitForSeconds(2f);
        //Damage Enemy
        DealDamage(e, p); //switches to the Damage Enemy function
    }*/

    //Used to delay the Player's Heal
    /*IEnumerator HealPlayerAllies()
    {
        yield return new WaitForSeconds(2f);
        //Heal Player (can be adjusted to work on summoned allies)
        HealAllies(p, e); //switches to the Damage Enemy function
    }*/

    //Used to delay the Player's Attack
    /*IEnumerator SketchEnemies()
    {
        print("Sketch");
        yield return new WaitForSeconds(2f);
        //Add "sketch the enemies" functinality
        MinionTurn();
    }*/

    //Used to delay the Player's Attack
    /*IEnumerator SummonPlayerAlly()
    {
        yield return new WaitForSeconds(2f);
        //Summon Ally for player
        SummonAllies(); //switches to the Damage Enemy function
    }*/

    //Checks which enemy was targeted and deals the correct damage
    /*public void DamageEnemy()
    {
        for (int i = 0; i < e.Length; i++)
        {
            if (e[i].targeted == true) //if the enemy is targeted
            {
                Debug.Log("Enemy " + i + " has been Damaged");

                e[i].currentHP -= p.HitValue; //apply the player's hit value
                Mathf.Clamp(e[i].currentHP, 0, e[i].maxHP);
                e[i].targeted = false; //Target Toggle switched to false
            }
            if (!e[i].isDead && e[i].currentHP <= 0) //if the enemy's health is less than or equal to 0
            {
                //Kill enemy
                Debug.Log("Enemy " + i + " is Dead");
                e[i].isDead = true; //isDead toogle on Enemy Object is toggled on
                CombatSystem.livingEnemies--;
            }
        }
        cs.EnemyDeadCheck();
    }*/

    /*void HealAllies()
    {
        print("Heal");
        p.currentHP += p.HitValue;
        if (p.currentHP >= p.maxHP)
        {
            p.currentHP = p.maxHP;
        }
        MinionTurn();
    }*/

    public void Sketch(Entity target, Entity user)
    {

    }

    public void SummonAllies(Entity target, Entity user)
    {
        int nMinions = MinionBehaviours.numMinions;
        //print("Summon");
        if (nMinions < p.maxMinions)
        {
            mb.NewMinion();

            a[nMinions + 1] = mb.minions[nMinions];
        }

        //Debug.Log("Summoned a minion");
    }

    /*void MinionTurn()
    {
        //Debug.Log("Start Minion Turn");
        for (int i = 0; i < numMinions; i++)
        {
            if (!minions[i].isDead) em.ChooseAction(e[Random.Range(0, e.Length)], minions[i], minions);
        }
        cs.EnemyDeadCheck(); //switches to Player Dead Check
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : EntityBehaviours
{
    
    public int numMinions = 0;
    public MoveChoice[] moves;

    private Player p;
    private Enemy[] e; //enemy party
    private Entity[] a = new Entity[5]; //ally party

    //Accessed libraries
    private CombatSystem cs;
    private EnemyMoves em;
    private PlayerButtons pb;
    private MinionBehaviours mb;
    private MoveChoice selectedMove;


    /*
     * A delegate stands in place of the method you want to call. It's useful for if you have a series of
     * related methods that take the same perameters. This way, you can access any of those methods in the 
     * line you call the delegate from.
     * 
     * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
     */

    public delegate void MoveChoice(Entity target, Entity user);

    public void Awake()
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
        p = player;
        pb.SetPlayer(p);
        e = enemies;
        a[0] = p;

        pb.PlayerNewTurn(0, e, a);
    }

    //Switch move that delegate will use
    private void Directory(int index)
    {
        selectedMove = moves[index];
    }

    public void UseMove(int index, Entity target, Entity user)
    {
        Directory(index);

        selectedMove(target, user); //This is the delegate being called

        cs.EnemyDeadCheck();

        mb.MinionTurn(1, e, a);
    }

    public void Sketch(Entity target, Entity user)
    {
        //For when the sketch button has functionality
    }

    public void SummonAllies(Entity target, Entity user)
    {
        int nMinions = MinionBehaviours.numMinions;

        //summon a new minion if they aren't all on the field
        if (nMinions < p.maxMinions)
        {
            mb.NewMinion();

            a[nMinions + 1] = mb.minions[nMinions];
        }
    }
}

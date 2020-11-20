using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    //Select target
    public Button[] enemySelect;
    public Button[] allySelect;
    private Button[] targetedPartyButtons;

    //Available moves
    public Button[] pActions, mActions;
    public Button[][] actions = new Button[4][];
    private delegate void useMove(int target);
    private useMove[] whichEntity;
    private useMove selectedEntity;

    //Active parties
    private Player p;
    private Entity[] a, targetedParty;
    private Enemy[] e;
    

    private bool wasEnemyParty = false;
    private int num, allyIndex;

    //Accessed classes
    private PlayerMoves pm;
    private EnemyMoves em;
    private MinionBehaviours mb;

    

    private void Awake()
    {
        pm = FindObjectOfType<PlayerMoves>();
        em = FindObjectOfType<EnemyMoves>();
        mb = FindObjectOfType<MinionBehaviours>();

        actions.SetValue(pActions, 0);

        whichEntity = new useMove[] { PlayerMove, MinionMove};
    }



    /// ACTION BUTTONS ///

    
    //Set up ally party move selection buttons
    public void PlayerNewTurn(int aI, Enemy[] enemies, Entity[] ally)
    {
        e = enemies;
        a = ally;
        allyIndex = aI;

        SetButtonsActive(true, allyIndex);

        bool isMaxAllies = pm.numMinions < 3;
        actions[allyIndex][1].gameObject.SetActive(isMaxAllies);

        selectedEntity = whichEntity[allyIndex > 0 ? 1 : 0];
    }

    //Use ally moves based on button number selected
    public void OnActionSelect()
    {
        string pos = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().name;
        int.TryParse(pos, out num);

        if (targetedParty != null) SelectTarget(false, wasEnemyParty);
        SelectTarget(true, num % 2 == 0);
        SetButtonsActive(false, allyIndex);
    }

    //Set accessibility of action buttons
    private void SetButtonsActive(bool isActive, int index)
    {
        index = index == 0 ? MinionBehaviours.numMinions : index - 1;

        for (int i = 0; i < actions[index].Length; i++)
            actions[index][i].gameObject.SetActive(false);

        index = index == MinionBehaviours.numMinions ? 0 : index + 1;

        for (int i = 0; i < actions[index].Length; i++)
            actions[index][i].gameObject.SetActive(isActive);
    }


    /// TARGET BUTTONS ///

    //Select an Enemy to Target
    private void SelectTarget(bool isActive, bool enemyParty)//isActive is for turning buttons on and off
    {
        if(isActive)
        {
            wasEnemyParty = enemyParty;
            targetedParty = enemyParty ? e : a;
            targetedPartyButtons = enemyParty ? enemySelect : allySelect;
        }

        for (int i = 0; i < targetedParty.Length; i++)
        {
            if (targetedParty[i] != null && !targetedParty[i].isDead)
            {
                targetedPartyButtons[i].gameObject.SetActive(isActive);
            }
        }
    }

    public void PerformActionSelected()
    {
        SelectTarget(false, wasEnemyParty); //hide all the buttons 

        int.TryParse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name, out int select);

        targetedParty[select].targeted = true; //Target Toggle on Enemy Object is set to true

        selectedEntity(select);
    }

    //Use an ally move(int whichMove, Entity target, Entity user)
    private void PlayerMove(int target)
    {
        pm.UseMove(num, targetedParty[target], p);
    }

    //Use an ally move(int whichMove, Entity target, Entity user)
    private void MinionMove(int target)
    {
        em.UseMove(num, targetedParty[target], a[allyIndex]);

        mb.MinionTurn(allyIndex + 1, e, a);
    }

    //Set Player object
    public void SetPlayer(Player player)
    {
        p = player;
    }

    //Set which ally's buttons are active
    public void SetAllyToButtons(int i)
    {
        actions[i] = mActions;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    public Button[] enemySelect;
    public Button[] allySelect;
    public Button[] pActions, mActions;
    public Button[][] actions = new Button[4][];
    private Button[] targetedPartyButtons;
    private Player p;
    private Entity[] a, targetedParty;
    private Enemy[] e;
    private PlayerMoves pm;
    private bool wasEnemyParty = false;
    private int num, allyIndex;
    private allyUseMove[] whichAlly;
    private allyUseMove selectedAlly;
    private EnemyMoves em;
    private MinionBehaviours mb;

    private delegate void allyUseMove(int target);

    private void Awake()
    {
        pm = FindObjectOfType<PlayerMoves>();
        em = FindObjectOfType<EnemyMoves>();
        mb = FindObjectOfType<MinionBehaviours>();

        actions.SetValue(pActions, 0);

        whichAlly = new allyUseMove[] { PlayerMove, MinionMove};
    }



    /// ACTION BUTTONS ///


    public void PlayerNewTurn(int aI, Enemy[] enemies, Entity[] ally)
    {
        e = enemies;
        a = ally;
        allyIndex = aI;

        SetButtonsActive(true, allyIndex);

        bool isMaxAllies = pm.numMinions < 3;
        actions[allyIndex][1].gameObject.SetActive(isMaxAllies);

        selectedAlly = whichAlly[allyIndex > 0 ? 1 : 0];
    }

    //Player actions based on button number selected
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
    void SelectTarget(bool isActive, bool enemyParty)
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
        //Debug.Log(select + " Targeted");
        targetedParty[select].targeted = true; //Target Toggle on Enemy Object is set to true

        selectedAlly(select);
    }

    private void PlayerMove(int target)
    {
        pm.UseMove(num, targetedParty[target], p);
    }

    private void MinionMove(int target)
    {
        em.UseMove(num, targetedParty[target], a[allyIndex]);

        mb.MinionTurn(allyIndex + 1, e, a);
    }

    public void SetPlayer(Player player)
    {
        p = player;
    }

    public void SetAllyToButtons(int i)
    {
        actions[i] = mActions;
    }
}

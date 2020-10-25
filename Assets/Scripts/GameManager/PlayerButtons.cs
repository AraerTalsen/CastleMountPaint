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
    public Button[] actions;
    private Button[] targetedPartyButtons;
    private Player p;
    private Entity[] a, targetedParty;
    private Enemy[] e;
    private PlayerMoves pm;
    private bool wasEnemyParty = false;
    private int num;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMoves>();
    }



    /// ACTION BUTTONS ///


    public void PlayerNewTurn(Player player, Enemy[] enemies, Entity[] ally)
    {
        p = player;
        e = enemies;
        a = ally;

        SetButtonsActive(true);

        bool isMaxAllies = pm.numMinions < 3;
        actions[1].gameObject.SetActive(isMaxAllies);
    }

    //Player actions based on button number selected
    public void OnActionSelect()
    {
        string pos = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().name;
        int.TryParse(pos, out num);

        if (targetedParty != null) SelectTarget(false, wasEnemyParty);
        SelectTarget(true, num % 2 == 0);
        //SetButtonsActive(false);
    }

    //Set accessibility of action buttons
    private void SetButtonsActive(bool isActive)
    {
        for (int i = 0; i < actions.Length; i++) actions[i].gameObject.SetActive(isActive);
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
        Debug.Log(select + " Targeted");
        targetedParty[select].targeted = true; //Target Toggle on Enemy Object is set to true

        pm.UseMove(num, targetedParty[select], p);
    }
}

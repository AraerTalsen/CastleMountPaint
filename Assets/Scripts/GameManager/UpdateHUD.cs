using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    //Allies
    public TextMeshProUGUI[] allyHP;
    public Slider[] allyHPSlider;

    //Enemies
    public TextMeshProUGUI[] enemyHP;
    public Slider[] enemyHPSlider;

    // Update is called once per frame
    public void UpdateEveryHUD()
    {
        UpdateAllyHUD();
        UpdateEnemyHUD();
    }

    private void UpdateAllyHUD()
    {
        Entity[] a = CombatSystem.allyParty;
        int numAllies = MinionBehaviours.numMinions + 1;


        for (int i = 0; i < numAllies; i++)
        {
            if (!a[i].isDead)
            {
                allyHP[i].text = "HP: " + a[i].currentHP;
                allyHPSlider[i].value = a[i].currentHP;
            }
        }
    }

    private void UpdateEnemyHUD()
    {
        Enemy[] e = CombatSystem.enemyParty;

        for (int i = 0; i < e.Length; i++)
        {
            if(!e[i].isDead)
            {
                enemyHP[i].text = "HP: " + e[i].currentHP;
                enemyHPSlider[i].value = e[i].currentHP;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviours : MonoBehaviour
{
    //Applies damage onto the target
    public void DealDamage(Entity t, Entity u)
    {
        if (t.targeted == true) //if the player is targeted
        {
            Debug.Log("Damage to " + t.eName);

            t.currentHP -= u.HitValue; //apply enemy's hit value to the player
            t.targeted = false; //the player is no longer targeted
        }
    }

    //Entity heals another using this
    public void HealAllies(Entity t, Entity u)
    {
        Debug.Log("Healing " + t.eName);

            if (!u.isDead)
            {
                u.currentHP += u.HitValue;
                u.currentHP = Mathf.Clamp(u.currentHP, 0, u.maxHP);
            }

        t.targeted = false; //the player is no longer targeted
    }
}

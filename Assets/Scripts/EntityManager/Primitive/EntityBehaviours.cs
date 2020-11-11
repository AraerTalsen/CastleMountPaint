using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move set that any entity can perform
public class EntityBehaviours : MonoBehaviour
{
    public void DealDamage(Entity t, Entity u)
    {
        t.currentHP -= u.HitValue;
    }

    public void HealAllies(Entity t, Entity u)
    {
        u.currentHP += u.HitValue;
        u.currentHP = Mathf.Clamp(u.currentHP, 0, u.maxHP);
    }
}

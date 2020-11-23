using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move set that any entity can perform
public class EntityBehaviours : MonoBehaviour
{
    public void DealDamage(Entity t, Entity u)
    {
        t.currentHP -= u.HitValue;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Player Damaged");
    }

    public void HealAllies(Entity t, Entity u)
    {
        t.currentHP += u.HitValue;
        t.currentHP = Mathf.Clamp(t.currentHP, 0, t.maxHP);
        print(t.eName);
    }

    public void Miss(Entity t, Entity u)
    {
        print(u.eName + " missed " + t.eName);
    }
}

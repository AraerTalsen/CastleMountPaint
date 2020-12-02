using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public string eName;
    public string Description;

    public int maxHP;
    public int currentHP;
    public int baseHitValue;
    public int HitValue;

    public bool targeted = false;
    public bool isDead = false;
}

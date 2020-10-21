using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public int maxHP;
    public int currentHP;
    public int HitValue;
    public bool targeted = false;
    public string eName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public string playerName;
    public string Description;

    public GameObject playerPrefab;

    public int maxHP;
    public int currentHP;
    public int HitValue;
    public int baseHitValue;

    public bool playerTargeted = false;
}

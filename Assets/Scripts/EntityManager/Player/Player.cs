using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : Entity
{
    //public string playerName;
    public string Description;

    public GameObject playerPrefab;

    //public int maxHP;
    //public int currentHP;
    //public int HitValue;
    public int baseHitValue;
    public int maxMinions = 3;

    //public bool playerTargeted = false;
}

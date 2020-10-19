using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public string Description;

    public bool enemyToggle = true;
    public bool enemyTargeted = false;
    public bool isDead = false;

    public GameObject enemyType;

    public int maxHP;
    public int currentHP;
    public int HitValues;
    public int baseHitValue;

    public Sprite enemySprite;

    public string personality;
    public int[] pWeightRange;
}

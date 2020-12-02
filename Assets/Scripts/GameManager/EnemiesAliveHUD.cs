using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesAliveHUD : MonoBehaviour
{
    public TextMeshProUGUI t;

    public void Start()
    {
        t.text = "Enemies: " + ActiveOverworldEntity.entityCount[0];
    }

}

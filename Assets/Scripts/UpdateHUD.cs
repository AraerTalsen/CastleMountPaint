using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    //Scriptable Objects
    public Player player1; //player scriptable object reference

    public Player summonedMinion1, summonedMinion2, summonedMinion3; //player summonable reference

    public Enemy enemy1; //enemy scriptable object references
    public Enemy enemy2;
    public Enemy enemy3;

    //UI Elements
    public TextMeshProUGUI playerHPText; //player UI
    public Slider playerHPSlider;

    public TextMeshProUGUI minionHPText1, minionHPText2, minionHPText3; //player UI
    public Slider minionHPSlider1, minionHPSlider2, minionHPSlider3;

    public TextMeshProUGUI enemy1HPText; //enemy UI
    public TextMeshProUGUI enemy2HPText;
    public TextMeshProUGUI enemy3HPText;

    public Slider enemy1HPSlider;
    public Slider enemy2HPSlider;
    public Slider enemy3HPSlider;

    //GameObjects
    public GameObject enemy1TextBoxGO;
    public GameObject enemy2TextBoxGO;
    public GameObject enemy3TextBoxGO;

    public GameObject enemy1SelectButtonGO;
    public GameObject enemy2SelectButtonGO;
    public GameObject enemy3SelectButtonGO;

    public GameObject enemy1NameTextGO;
    public GameObject enemy2NameTextGO;
    public GameObject enemy3NameTextGO;

    public GameObject enemy1HPTextGO;
    public GameObject enemy2HPTextGO;
    public GameObject enemy3HPTextGO;

    public GameObject enemy1HPSliderGO;
    public GameObject enemy2HPSliderGO;
    public GameObject enemy3HPSliderGO;

    private void Start()
    {
        
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        //Updates all the UI elements
        playerHPText.text = "HP: " + player1.currentHP; //Player HP Text Update

        enemy1HPText.text = "HP: " + enemy1.currentHP; //Enemy HP Text Update
        enemy2HPText.text = "HP: " + enemy2.currentHP;
        enemy3HPText.text = "HP: " + enemy3.currentHP;

        playerHPSlider.value = player1.currentHP; //Player HP Slider Update

        enemy1HPSlider.value = enemy1.currentHP; //Enemy HP Slider Update
        enemy2HPSlider.value = enemy2.currentHP;
        enemy3HPSlider.value = enemy3.currentHP;

        //Enemy IsDead check
        if(enemy1.isDead == true) //if the enemies isDead toggle is true
        {
            //Hide all UI elements associated
            enemy1SelectButtonGO.SetActive(false);
            enemy1NameTextGO.SetActive(false);
            enemy1HPTextGO.SetActive(false);
            enemy1HPSliderGO.SetActive(false);
            enemy1TextBoxGO.SetActive(false);
        }
        /*if (enemy2.isDead == true)
        {
            //Hide all UI elements associated
            enemy2SelectButtonGO.SetActive(false);
            enemy2NameTextGO.SetActive(false);
            enemy2HPTextGO.SetActive(false);
            enemy2HPSliderGO.SetActive(false);
            enemy2TextBoxGO.SetActive(false);
        }*/
        /*if (enemy3.isDead == true)
        {
            //Hide all UI elements associated
            enemy3SelectButtonGO.SetActive(false);
            enemy3NameTextGO.SetActive(false);
            enemy3HPTextGO.SetActive(false);
            enemy3HPSliderGO.SetActive(false);
            enemy3TextBoxGO.SetActive(false);
        }
    }*/
}

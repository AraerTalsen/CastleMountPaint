﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player overworld movement script
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer rend;

    //sets the enemy that the player collided with - loads that enemy type
    public static bool enemy1Combat, enemy2Combat, enemy3Combat = false;

    //sets the speed the player moves at
    public float playerSpeed = 10.0f;

    public ListCreator UpdateMinionInventoryFunction;

    public static bool firstTime = true;
    private static bool playerExists = false;
    //public static Vector2 lastMove;

    void Start()
    {
        //if (!playerExists)
        //{
        //    playerExists = true;
        //    //When a new level is loaded Player does not get deleted
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        enemy1Combat = false;
        enemy2Combat = false;
        enemy3Combat = false;

        rend.enabled = true;
    }

    //Takes the wasd and arrow keys for movement in 8 directions
    void Update()
    {
        //Player Movement//
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //Up
                body.velocity = new Vector2(0, playerSpeed);
                anim.SetInteger("Direction", 1); //animation change
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //Down
                body.velocity = new Vector2(0, -playerSpeed);
                anim.SetInteger("Direction", 3);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Left
                body.velocity = new Vector2(-playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(-1, 1, 1); //flip the sprite
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //Right
                body.velocity = new Vector2(playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(1, 1, 1); //flip the sprite
            }

            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                //No Input
                body.velocity = new Vector2(0, 0);
                anim.SetInteger("Direction", 0);
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy1Sketch")
        {
            ListCreator.numberOfItemsCollected++;
            ListCreator.runInventoryUpdate = true;
            UpdateMinionInventoryFunction.InsertNewMinion();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "enemy2Sketch")
        {
            ListCreator.numberOfItemsCollected++;
            ListCreator.runInventoryUpdate = true;
            UpdateMinionInventoryFunction.InsertNewMinion();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "enemy3Sketch")
        {
            ListCreator.numberOfItemsCollected++;
            ListCreator.runInventoryUpdate = true;
            UpdateMinionInventoryFunction.InsertNewMinion();
            Destroy(other.gameObject);
        }
    }
}

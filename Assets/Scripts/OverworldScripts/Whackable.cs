﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whackable : MonoBehaviour
{
    public Color c;
    public int type, id;
    public bool minion, on = true, whackable = false;
    public Collider2D canvas;

    public void GetWhacked()
    {
        on = false;
        ActiveOverworldEntity.entityInDimension[1][type][id] = false;
        gameObject.SetActive(false);
    }

    private void SuckPaint()
    {
        on = false;
        ActiveOverworldEntity.entityInDimension[1][type][id] = false;
        //if (Player.currentPaint < Player.maxPaint) Player.currentPaint++;
        gameObject.SetActive(false);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(whackable)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(collision.IsTouching(canvas))
                    GetWhacked();
            }
        }
            else if (Input.GetKey(KeyCode.Q))
                SuckPaint(); 
    }*/
}

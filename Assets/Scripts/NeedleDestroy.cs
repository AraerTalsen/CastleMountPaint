﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeedleDestroy : MonoBehaviour
{
    private Animator anim;

    public static bool miss = false;
    public static bool hit = false;
    public static bool crit = false;

    public static bool canAct;

    public GameObject missText;
    public GameObject hitText;
    public GameObject critText;

    public Transform textSpawnArea;

    private PlayerButtons pb;
    private int success = 0;
    private bool reseting = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pb = FindObjectOfType<PlayerButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAct == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position.x <= 2.2f && transform.position.x >= -2.2f && !(transform.position.x <= 0.5f && transform.position.x >= -0.5f))
            {
                success = 1;
                Debug.Log("Hit");

                canAct = false;
                hit = true;

                NeedleMove.needleSpeed = 0f;
                anim.Play("HitBump");
                StartCoroutine(PauseNeedle());
            }
            else if(transform.position.x <= 0.5f && transform.position.x >= -0.5f)
            {
                success = 2;
                Debug.Log("Crit");

                canAct = false;
                crit = true;

                NeedleMove.needleSpeed = 0f;
                anim.Play("HitBump");
                StartCoroutine(PauseNeedle());
            }
            else if(!(transform.position.x <= 0.5f && transform.position.x >= -0.5f) || !(transform.position.x <= 2.2f && transform.position.x >= -2.2f))
            {
                success = 0;
                Debug.Log("Miss");

                canAct = false;
                miss = true;

                NeedleMove.needleSpeed = 0f;
                anim.Play("MissFade");
                StartCoroutine(PauseNeedle());
            }
        }
    }

    public void Reset()
    {
        reseting = true;
        StartCoroutine(PauseNeedle());
    }

    IEnumerator PauseNeedle()
    {
        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
        NeedleMove.needleSpeed = NeedleMove.speedReset;
        SpawnNeedle.spawnedNeedle = false;
        if (!reseting)
            pb.SkillCheck(success);
        else reseting = false;

        //if (miss)
        //{
        //    Instantiate(missText, textSpawnArea);
        //}
        //else if (hit)
        //{
        //    Instantiate(hitText, textSpawnArea);
        //}
        //else if (crit)
        //{
        //    Instantiate(critText, textSpawnArea);
        //}

        crit = false;
        hit = false;
        miss = false;

        canAct = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BarEnd"))
        {
            Debug.Log("Miss");
            miss = true;
            SpawnNeedle.spawnedNeedle = false;
            Destroy(this.gameObject);
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Hit"))
    //    {
    //        Debug.Log("Go");
    //        if (Input.GetKeyDown(KeyCode.H))
    //        {
    //            Debug.Log("Hit");
    //            SpawnNeedle.spawnedNeedle = false;
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
}

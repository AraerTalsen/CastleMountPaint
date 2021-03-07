using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed;

    public bool playerInRange;
    public bool canPatrol = true;
    public bool canMove = true;
    public bool newValue = true;

    public float moveX;
    public float moveY;

    public GameObject alertSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            canMove = false;

            alertSprite.SetActive(true);
        }

        if (!playerInRange && canPatrol)
        {
            StartCoroutine(Patrol());
        }

        if (canMove)
        {
            //anim.SetBool("isMoving", true);

            float step = (speed/2) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveX, moveY), step);
        }
    }

    IEnumerator Patrol()
    {
        canPatrol = false;
        if (newValue == true)
        {
            NewValue();
        }

        //anim.SetBool("isMoving", false);
        canMove = false;

        yield return new WaitForSeconds(2f);

        canMove = true;

        yield return new WaitForSeconds(2f);

        canPatrol = true;
        newValue = true;
    }

    public void NewValue()
    {
        moveX = Random.Range(-13f, 13f);
        moveY = Random.Range(-13f, 13f);
        newValue = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alertSprite.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alertSprite.SetActive(false);
            playerInRange = false;
            //anim.SetBool("isMoving", false);
        }
    }
}

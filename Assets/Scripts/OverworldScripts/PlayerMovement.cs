using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//player overworld movement script
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    public bool facingRight;

    //horizontal and vertical floats for player movement
    float hor;
    float vert;
    float moveLimiter = 0.7f;

    //sets the enemy that the player collided with - loads that enemy type
    public static bool enemy1Combat, enemy2Combat, enemy3Combat = false;

    //sets the speed the player moves at
    public float playerSpeed = 2.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        facingRight = true;
        enemy1Combat = false;
        enemy2Combat = false;
        enemy3Combat = false;
        
    }

    //Takes the wasd and arrow keys for movement in 8 directions
    void Update()
    {
        // Gives a value between -1 and 1
        hor = Input.GetAxisRaw("Horizontal"); // -1 is left
        vert = Input.GetAxisRaw("Vertical"); // -1 is down

        if (hor > 0 && !facingRight)
        {
            changeDirectionFacing();
        } else if (hor < 0 && facingRight)
        {
            changeDirectionFacing();
        }
    }

    //every frame, move the dude on screen
    void FixedUpdate()
    {
        // if player is moving vertically, slow them down a bit
        if (hor != 0 && vert != 0) 
        {
            //sets them to .7 move speed
            hor *= moveLimiter;
            vert *= moveLimiter;
        }

        body.velocity = new Vector2(hor * playerSpeed, vert * playerSpeed);
    }

    void changeDirectionFacing()
    {
        facingRight = !facingRight;

        var imageDirection = transform.localScale;

        imageDirection.x *= -1;

        transform.localScale = imageDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "painting1")
        {
            SceneManager.LoadScene(4);
        }

        if (collision.gameObject.tag == "enemy1Sketch")
        {
            PlayerMinionInventory.sketch1Active = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "enemy2Sketch")
        {
            PlayerMinionInventory.sketch2Active = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "enemy3Sketch")
        {
            PlayerMinionInventory.sketch3Active = true;
            Destroy(collision.gameObject);
        }
    }
}

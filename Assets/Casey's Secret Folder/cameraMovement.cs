using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, speed);
        }

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.SetActive(true);
        }
    }
}

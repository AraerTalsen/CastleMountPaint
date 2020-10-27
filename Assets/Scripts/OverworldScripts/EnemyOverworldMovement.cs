using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOverworldMovement : MonoBehaviour
{
    public float speed;
    public Vector2 target;
    public Vector2 position;
    public GameObject PlayerPosition;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        target = new Vector2(PlayerPosition.transform.position.x, PlayerPosition.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
}

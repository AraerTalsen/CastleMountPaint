using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyOverworldMovement : MonoBehaviour
{
    public float speed;
    public Vector2 target;
    public Vector2 position;
    public GameObject PlayerPosition;
    public Enemy[] party = new Enemy[3];//Which enemies will appear in combat

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        target = new Vector2(PlayerPosition.transform.position.x, PlayerPosition.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    //Start combat if collide with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            CombatSystem.enemyParty = party;
            SceneManager.LoadScene(1);
        }
    }
}

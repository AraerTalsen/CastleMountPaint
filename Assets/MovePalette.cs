using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePalette : MonoBehaviour
{
    public Transform movePoint;
    public GameObject palette;

    public GameObject enemyInventory;
    public Transform enemyMovePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        LeanTween.move(palette, movePoint, 0.5f).setEaseInSine();
        StartCoroutine(MoveUIElements());
    }

    IEnumerator MoveUIElements()
    {
        yield return new WaitForSeconds(0.3f);
        LeanTween.move(enemyInventory, enemyMovePoint, 0.5f).setEaseInSine();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePalette : MonoBehaviour
{
    public Transform movePoint;
    public GameObject inventory;

    public Transform retractPoint;

    //public GameObject enemyInventory;
    //public Transform enemyMovePoint;

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Sweep");
        LeanTween.move(inventory, movePoint, 0.5f).setEaseInSine();
        //StartCoroutine(MoveUIElements());
    }

    public void Retract()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/SweepDown");
        LeanTween.move(inventory, retractPoint, 0.3f).setEaseInSine();
    }

    //IEnumerator MoveUIElements()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    LeanTween.move(enemyInventory, enemyMovePoint, 0.5f).setEaseInSine();

    //}
}

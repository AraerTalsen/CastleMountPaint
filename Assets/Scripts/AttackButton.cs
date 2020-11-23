using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{

    public GameObject AttackBarGO;

    public Transform AttackBarPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AttackButtonFunctionality()
    {
        Instantiate(AttackBarGO, AttackBarPos);

        Debug.Log("YO");
    }
}

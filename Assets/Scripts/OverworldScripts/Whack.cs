using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whack : MonoBehaviour
{
    public bool active = false;

    private void OnTriggerStay2D(Collider2D c)
    {
        if (active && c.GetComponent<Whackable>())
        {
            c.GetComponent<Whackable>().GetWhacked();
        }
    }
}

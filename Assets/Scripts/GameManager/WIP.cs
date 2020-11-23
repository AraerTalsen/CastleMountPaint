using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIP : MonoBehaviour
{
    public GameObject specialAttack, inventory;

    public void Button1()
    {
        StartCoroutine("Special");
    }

    public void Button2()
    {
        StartCoroutine("Item");
    }

    public IEnumerator Special()
    {
        specialAttack.SetActive(true);
        yield return new WaitForSeconds(3f);
        specialAttack.SetActive(false);
    }

    public IEnumerator Item()
    {
        inventory.SetActive(true);
        yield return new WaitForSeconds(3f);
        inventory.SetActive(false);
    }
}

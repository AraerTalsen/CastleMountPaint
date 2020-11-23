using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtons : MonoBehaviour
{
    public GameObject AttackButton;
    public GameObject SpecialButton;
    public GameObject ItemsButton;
    public GameObject RunButton;

    public GameObject CloneAttackButton;
    public GameObject CloneSpecialButton;
    public GameObject CloneItemsButton;
    public GameObject CloneRunButton;

    public Transform spawnPoint;

    public bool canSpawn = true;

    // Start is called before the first frame update
    /*void Start()
    {
        canSpawn = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            Spawn();
        else if (Input.GetKeyUp(KeyCode.S))
            Retract();
    }

    public void Spawn()
    {
        if (canSpawn)
        {
            AttackButton.SetActive(true);
            SpecialButton.SetActive(true);
            ItemsButton.SetActive(true);
            RunButton.SetActive(true);

            canSpawn = false;
        }
    }

    public void Retract()
    {
        if (!canSpawn)
        {
            AttackButton.GetComponent<Button>().interactable = false;
            SpecialButton.GetComponent<Button>().interactable = false;
            ItemsButton.GetComponent<Button>().interactable = false;
            RunButton.GetComponent<Button>().interactable = false;

            LeanTween.move(AttackButton, spawnPoint, 0.5f).setEaseInSine();
            LeanTween.move(SpecialButton, spawnPoint, 0.5f).setEaseInSine();
            LeanTween.move(ItemsButton, spawnPoint, 0.5f).setEaseInSine();
            LeanTween.move(RunButton, spawnPoint, 0.5f).setEaseInSine();

            StartCoroutine(DestroyButtons());
        }
    }

    IEnumerator DestroyButtons()
    {
        LeanTween.scale(AttackButton, new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(SpecialButton, new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(ItemsButton, new Vector3(0, 0, 0), 0.5f);
        LeanTween.scale(RunButton, new Vector3(0, 0, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        AttackButton.SetActive(false);
        SpecialButton.SetActive(false);
        ItemsButton.SetActive(false);
        RunButton.SetActive(false);

        canSpawn = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRange = 1.7f;

    public GameObject interactIcon;

    // Start is called before the first frame update
    void Start()
    {
        interactIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(gameObject.transform.position, OverworldManager.instance.Player.position) < interactRange)
        { 
            interactIcon.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E) && !DialogueManager.inDialogue)
            {
                Interact();
            }
        }
        else
        {
            interactIcon.SetActive(false);
        }
    }

    public virtual void Interact()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange );
    }
}

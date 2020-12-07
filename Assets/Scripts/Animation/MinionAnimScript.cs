using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimScript : MonoBehaviour
{
    public Transform movePoint;

    public Transform retractPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MinionAnimTime()
    {
        LeanTween.move(this.gameObject, movePoint, 1f);
    }

    public void MinionRetract()
    {
        LeanTween.move(this.gameObject, retractPoint, 1f);
    }
}

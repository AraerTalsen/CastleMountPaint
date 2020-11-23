using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombatAnim : MonoBehaviour
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

    public void AnimTime()
    {
        LeanTween.move(this.gameObject, movePoint, 0.3f);
    }

    public void Retract()
    {
        LeanTween.move(this.gameObject, retractPoint, 0.3f);
    }
}

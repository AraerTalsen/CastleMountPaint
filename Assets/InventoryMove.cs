using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMove : MonoBehaviour
{
    public Transform movePoint;

    // Start is called before the first frame update
    //void Start()
    //{
    //    LeanTween.move(this.gameObject, movePoint, 0.3f);
    //}

    private void OnEnable()
    {
        LeanTween.move(this.gameObject, movePoint, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerEnterHandler
{
    //Script to control all UI Sounds

    //Button Select Sounds (OnCLick)
    public void SelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/UI Select");
    }

    //On Button Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/UI Hover");
    }
}

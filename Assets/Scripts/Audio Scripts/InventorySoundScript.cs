using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySweep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Sweep");
    }

    public void PlayShakeLeft()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/ShakeLeft");
    }

    public void PlayShakeRight()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/ShakeRight");
    }

    public void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Click");
    }

    public void PlayOpen()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Open");
    }

    public void PlayClose()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Close");
    }

}

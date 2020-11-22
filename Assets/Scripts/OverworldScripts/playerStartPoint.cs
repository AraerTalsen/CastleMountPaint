using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStartPoint : MonoBehaviour
{
    private PlayerMovement player;
    private CameraFollow mainCamera;

    //1 for Back //2 for Side //3 for Front
    public int playerDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerMovement.firstTime == false)
        {
            //player = FindObjectOfType<PlayerMovement>();
            //player.transform.position = transform.position;

            //mainCamera = FindObjectOfType<CameraFollow>();
            //mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

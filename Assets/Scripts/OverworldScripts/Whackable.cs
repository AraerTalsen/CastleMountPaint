using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whackable : MonoBehaviour
{
    public Color c;
    public int type, id;
    public bool minion, on = true;
    private static PlayerMovement pm;
    private Transform pos;

    public Vector2 pFacing, meToPlayer;
    public Whackable[] w;

    /*private void Awake()
    {
        if (!LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num])
        {
            id = ActiveOverworldEntity.AddEntity(type, this);
        }
        else if (on) on = ActiveOverworldEntity.entityInDimension[1][type].Find();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(on);
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && PlayerInRange() && FacingMe())
            GetWhacked();
    }

    private bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, pm.transform.position) <= 1.3f;
    }

    private bool FacingMe()
    {
        pFacing = pm.facing;
        meToPlayer = (pm.transform.position + transform.position).normalized;

        bool a = pFacing.x >= meToPlayer.x - .1f && pFacing.x <= meToPlayer.x + .1f;
        bool b = pFacing.y >= meToPlayer.y - .1f && pFacing.y <= meToPlayer.y + .1f;

        return a && b;
    }

    private void GetWhacked()
    {
        on = false;
        gameObject.SetActive(false);
    }

    public void UpdateEntity()
    {
        gameObject.SetActive(on);
    }
}

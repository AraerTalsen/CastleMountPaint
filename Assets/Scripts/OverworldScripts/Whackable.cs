using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whackable : MonoBehaviour
{
    public Color c;
    public int num;
    public bool minion;
    private static PlayerMovement pm;
    private Transform pos;

    public Vector2 pFacing, meToPlayer;

    private void Awake()
    {
        if(!LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num])
        {
            ActiveOverworldEntity.entityInDimension[0][1].Add(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(ActiveOverworldEntity.entityInDimension[0][1][num]);
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
        ActiveOverworldEntity.entityInDimension[0][1][num].SetActive(false);
        gameObject.SetActive(false);
    }
}

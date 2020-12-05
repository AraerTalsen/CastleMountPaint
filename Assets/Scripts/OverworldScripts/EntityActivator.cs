using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActivator : MonoBehaviour
{
    private Whackable[] whack;
    // Start is called before the first frame update
    void Awake()
    {
        whack = FindObjectsOfType<Whackable>();

        if (!LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num])
        {
            for (int i = 0; i < whack.Length; i++)
                whack[i].id = ActiveOverworldEntity.AddEntity(whack[i].type, whack[i]);
        }
        else
        {
            Alt();
        }
            
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)) Alt();
    }

    private void Alt()
    {
        List<Whackable>[] w =
            {
                new List<Whackable>(),
                new List<Whackable>()
            };

        for (int i = 0; i < whack.Length; i++)
            w[whack[i].type].Add(whack[i]);

        for (int i = 0; i < w.Length; i++)
            ActiveOverworldEntity.LoadActive(w[i]);
    }
}

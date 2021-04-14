using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : Spawner
{
    // Start is called before the first frame update
    public void DisableEye(int idx)
    {
        DisableObject(idx);
    }

    public void DisableEye(int startIdx, int endIdx)
    {
        for(int i = startIdx; i < endIdx; i++)
        {
            DisableObject(i);
        }
    }
}

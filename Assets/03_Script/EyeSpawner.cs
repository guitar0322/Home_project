using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : Spawner
{
    // Start is called before the first frame update

    public void SpawnEye()
    {
        InitSpawn();
        for (int i = 0; i < spawnedObjectSet.transform.childCount; i++)
        {
            SpawnObject();
        }
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : Spawner
{
    // Start is called before the first frame update

    public void SpawnEye()
    {
        for (int i = 0; i < spawnedObjectSet.transform.childCount; i++)
        {
            SpawnObject();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnedObjectSet;
    public Transform spawnedPosSet;
    protected GameObject spawnedObject;

    private int IsValidObject()
    {
        for (int i = 0; i < spawnedObjectSet.childCount; i++)
        {
            if (spawnedObjectSet.GetChild(i).gameObject.activeSelf == false)
            {
                return i;
            }
        }
        return -1;
    }

    public void SpawnObject()
    {
        int objectIdx, objectPosIdx;
        objectIdx = IsValidObject();
        if (objectIdx == -1)
        {
            Debug.Log("invalid spawn num");
            return;
        }
        objectPosIdx = Random.Range(0, spawnedPosSet.transform.childCount);
        spawnedObject = spawnedObjectSet.GetChild(objectIdx).gameObject;
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = spawnedPosSet.GetChild(objectPosIdx).transform.position;
    }

    public void DisableObject(int idx)
    {
        spawnedObjectSet.GetChild(idx).gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

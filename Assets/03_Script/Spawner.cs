using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnedObjectSet;
    public Transform spawnedPosSet;
    protected GameObject spawnedObject;
    protected bool[] isSpawnPos;

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

    public int SpawnObject()
    {
        int objectIdx, objectPosIdx;
        objectIdx = IsValidObject();
        if (objectIdx == -1)
        {
            Debug.Log("invalid spawn num");
            return -1;
        }
        objectPosIdx = Random.Range(0, spawnedPosSet.transform.childCount);
        while(isSpawnPos[objectPosIdx] == true)
        {
            objectPosIdx = (objectPosIdx + 1) % spawnedPosSet.childCount;
        }
        isSpawnPos[objectPosIdx] = true;
        spawnedObject = spawnedObjectSet.GetChild(objectIdx).gameObject;
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = spawnedPosSet.GetChild(objectPosIdx).transform.position;
        for (int i = 0; i < spawnedObjectSet.childCount; i++)
        {
            Debug.Log(isSpawnPos[i]);
        }
        return objectPosIdx;
    }

    public void InitSpawn()
    {
        for(int i = 0; i < isSpawnPos.Length; i++)
        {
            isSpawnPos[i] = false;
        }
    }

    public void DisableObject(int idx)
    {
        spawnedObjectSet.GetChild(idx).gameObject.SetActive(false);
        for (int i = 0; i < spawnedObjectSet.childCount; i++)
        {
            Debug.Log(isSpawnPos[i]);
        }
    }

    public void SetObjectTransform(Transform target)
    {
        spawnedObject.transform.position = target.position;
        spawnedObject.transform.eulerAngles = target.eulerAngles;
    }
    // Start is called before the first frame update
    void Start()
    {
        isSpawnPos = new bool[spawnedPosSet.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

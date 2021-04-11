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

    public int SpawnObject(int num)
    {
        int objectIdx, objectPosIdx = -1;
        for(int i = 0; i < num; i++){
            objectIdx = IsValidObject();
            if (objectIdx == -1)
            {
                Debug.Log("invalid spawn num");
                return -1;
            }
            objectPosIdx = Random.Range(0, spawnedPosSet.transform.childCount);
            while (isSpawnPos[objectPosIdx] == true)
            {
                objectPosIdx = (objectPosIdx + 1) % spawnedPosSet.childCount;
            }
            isSpawnPos[objectPosIdx] = true;
            spawnedObject = spawnedObjectSet.GetChild(objectIdx).gameObject;
            spawnedObject.GetComponent<SpawnInfo>().posIdx = objectPosIdx;
            spawnedObject.SetActive(true);
            spawnedObject.transform.position = spawnedPosSet.GetChild(objectPosIdx).transform.position;
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
        GameObject disableObject = spawnedObjectSet.GetChild(idx).gameObject;
        disableObject.SetActive(false);
        isSpawnPos[disableObject.GetComponent<SpawnInfo>().posIdx] = false;
    }

    public void DisableObject(int startIdx, int endIdx){
        GameObject disableObject;
        for(int i = startIdx; i < endIdx; i++){
            disableObject = spawnedObjectSet.GetChild(i).gameObject;
            disableObject.SetActive(false);
            isSpawnPos[disableObject.GetComponent<SpawnInfo>().posIdx] = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class TMNQSpawner : Spawner
{
    public Transform THMNQPosSet;
    public Transform THMNQSet;
    public float TMNQSpawnTargetTime;
    public bool THspawnFlag;
    private int THSpawnNum = 0;
    float spawnCoolTime;
    float time = 0;


    public void DisableMNQ(int idx)
    {
        int posIdx = spawnedObjectSet.GetChild(idx).GetComponent<LassoMNQ>().posIdx;
        isSpawnPos[posIdx] = false;
        DisableObject(idx);
    }

    public void DisableMNQ(int startIdx, int endIdx)
    {
        int posIdx;
        for (int i = startIdx; i < endIdx; i++)
        {
            posIdx = spawnedObjectSet.GetChild(i).GetComponent<LassoMNQ>().posIdx;
            isSpawnPos[posIdx] = false;
            DisableObject(i);
        }
    }

    public void StopMNQ()
    {
        for (int i = 0; i < spawnedObjectSet.transform.childCount; i++)
        {
            spawnedObjectSet.transform.GetChild(i).GetComponent<NavMNQ>().StopFollow();
        }
    }

    public void SwapTHMNQ()
    {
        spawnedObjectSet = THMNQSet;
        spawnedPosSet = THMNQPosSet;
        THspawnFlag = true;
        spawnCoolTime = Random.Range(GameManager.instance.waitSpawnMNQMinTime, GameManager.instance.waitSpawnMNQMaxTime);
    }

    private void Update()
    {
    }
}

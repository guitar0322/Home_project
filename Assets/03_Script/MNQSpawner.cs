using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQSpawner : Spawner
{
    public Transform THMNQPosSet;
    public Transform THMNQSet;
    public bool THspawnFlag;
    private int THSpawnNum = 0;
    float spawnCoolTime;
    float time = 0;
    public void SpawnMNQ(int num, bool setSpeedFlag)
    {
        if(THspawnFlag)
        {
            if (THSpawnNum >= spawnedObjectSet.childCount)
            {
                return;
            }
            THSpawnNum += num;
        }
        for(int i = 0; i < num; i++)
        {
            SpawnObject();
            if(GameManager.instance.gameState < State.T_END_DOOR)
                spawnedObject.GetComponent<TMNQ>().SetProperty(GameManager.instance.TMNQWaitMinTime, GameManager.instance.TMNQWaitMaxTime, setSpeedFlag);
        }
    }

    public void DisableMNQ(int idx)
    {
        DisableObject(idx);
    }

    public void DisableMNQ(int startIdx, int endIdx)
    {
        for(int i = startIdx; i < endIdx; i++)
        {
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
        if(THspawnFlag == true)
        {
            time += Time.deltaTime;
            if(time >= spawnCoolTime)
            {
                SpawnMNQ(Random.Range(GameManager.instance.spawnMNQMinNum, GameManager.instance.spawnMNQMaxNum), false);
                time = 0;
                spawnCoolTime = Random.Range(GameManager.instance.waitSpawnMNQMinTime, GameManager.instance.waitSpawnMNQMaxTime);
            }
        }
    }
}

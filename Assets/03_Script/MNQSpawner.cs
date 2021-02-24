using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQSpawner : Spawner
{
    // Start is called before the first frame update
    public void SpawnMNQ(int num)
    {
        bool setSpeedFlag;
        setSpeedFlag = GameManager.instance.gameState < State.T_MNQ_THIRD ? false : true;
        for(int i = 0; i < num; i++)
        {
            SpawnObject();
            spawnedObject.GetComponent<TMNQ>().SetProperty(GameManager.instance.TMNQWaitMinTime, GameManager.instance.TMNQWaitMaxTime, setSpeedFlag);
        }
    }

    public void StopMNQ()
    {
        for (int i = 0; i < spawnedObjectSet.transform.childCount; i++)
        {
            spawnedObjectSet.transform.GetChild(i).GetComponent<NavMNQ>().StopFollow();
        }
    }
}

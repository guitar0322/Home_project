using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THMNQSpawner : Spawner
{
    float spawnCoolTime;
    int spawnNum;
    int spawnTotal;
    float time;
    private void OnEnable()
    {
        spawnCoolTime = Random.Range(GameManager.instance.waitSpawnTHMNQMinTime, GameManager.instance.waitSpawnTHMNQMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnCoolTime && spawnTotal < spawnedObjectSet.childCount)
        {
            spawnNum = Random.Range(GameManager.instance.spawnTHMNQMinNum, GameManager.instance.spawnTHMNQMaxNum);
            if(spawnTotal + spawnNum > spawnedObjectSet.childCount)
            {
                spawnNum = spawnedObjectSet.childCount - spawnTotal;
            }
            spawnTotal += spawnNum;
            SpawnObject(spawnNum);
            time = 0;
        }
    }
}

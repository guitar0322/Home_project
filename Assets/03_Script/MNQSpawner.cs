using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQSpawner : Spawner
{
    public Transform THMNQPosSet;
    public Transform THMNQSet;
    public float TMNQSpawnTargetTime;
    public bool THspawnFlag;
    private int THSpawnNum = 0;
    float spawnCoolTime;
    float time = 0;
    float TMNQSpawnTime;
    float TMNQDestroyTime;
    float TMNQDestroyTargetTime;
    public bool isSpawnTMNQ;

    public bool isSpotMNQ;

    public void SpawnMNQ(int num, bool setSpeedFlag)
    {
        int posIdx;
        if(GameManager.instance.gameState >= State.T_DOOR && GameManager.instance.gameState <= State.T_SPOT_THIRD){
            isSpawnTMNQ = true;
            TMNQDestroyTargetTime = Random.Range(GameManager.instance.mnqDestroyMinTime, GameManager.instance.mnqDestroyMaxTime);
            TMNQSpawnTime = 0;
        }
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
            posIdx = SpawnObject();
            Debug.Log("posIdx : " + posIdx);
            if (posIdx == -1)
                return;
            if (GameManager.instance.gameState < State.T_END_DOOR)
                spawnedObject.GetComponent<TMNQ>().SetProperty(GameManager.instance.TMNQWaitMinTime, GameManager.instance.TMNQWaitMaxTime, setSpeedFlag, posIdx);
            else
                spawnedObject.GetComponent<TMNQ>().posIdx = posIdx;
        }
    }

    public void DisableMNQ(int idx)
    {
        int posIdx = spawnedObjectSet.GetChild(idx).GetComponent<TMNQ>().posIdx;
        Debug.Log("posIdx : " + posIdx);
        isSpawnPos[posIdx] = false;
        DisableObject(idx);
        TMNQDestroyTime = 0;
    }

    public void DisableMNQ(int startIdx, int endIdx)
    {
        int posIdx;
        for (int i = startIdx; i < endIdx; i++)
        {
            posIdx = spawnedObjectSet.GetChild(i).GetComponent<TMNQ>().posIdx;
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

        if(isSpawnTMNQ == true && isSpotMNQ == false){
            TMNQDestroyTime += Time.deltaTime;
            if(TMNQDestroyTime >= TMNQDestroyTargetTime){
                DisableMNQ(0);
                TMNQSpawnTargetTime = Random.Range(GameManager.instance.mnqSpawnMinTime, GameManager.instance.mnqSpawnMaxTime);
                isSpawnTMNQ = false;
            }
        }
        else if(isSpawnTMNQ == false && GameManager.instance.gameState >= State.T_DOOR && GameManager.instance.gameState <= State.T_SPOT_THIRD){
            TMNQSpawnTime += Time.deltaTime;
            if(TMNQSpawnTime >= TMNQSpawnTargetTime){
                SpawnMNQ(1, false);
            }
        }
    }
}

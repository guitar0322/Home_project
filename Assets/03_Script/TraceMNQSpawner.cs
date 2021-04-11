using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMNQSpawner : Spawner
{
    public Transform TMNQPosSet;
    int posSetIdx = 0;
    public float TMNQSpawnTargetTime;
    float TMNQSpawnTime;
    float TMNQDestroyTime;
    float TMNQDestroyTargetTime;
    public bool isSpawnMNQ;
    public bool isSpotMNQ;
    private void OnEnable() {
        TMNQSpawnTargetTime = Random.Range(GameManager.instance.mnqSpawnMinTime, GameManager.instance.mnqSpawnMaxTime);
    }
    public void SpawnMNQ()
    {
        SpawnObject(1);
        isSpawnMNQ = true;
        TMNQDestroyTargetTime = Random.Range(GameManager.instance.mnqDestroyMinTime, GameManager.instance.mnqDestroyMaxTime);
        TMNQSpawnTime = 0;
    }

    public void DisableMNQ()
    {
        DisableObject(0);
        isSpawnMNQ = false;
        isSpotMNQ = false;
        TMNQDestroyTime = 0;
    }

    public void ChangeNextPosSet(){
        spawnedPosSet = TMNQPosSet.GetChild(++posSetIdx);
    }
    public void StopMNQ()
    {
        spawnedObjectSet.transform.GetChild(0).GetComponent<NavMNQ>().StopFollow();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawnMNQ == true && isSpotMNQ == false){
            TMNQDestroyTime += Time.deltaTime;
            if(TMNQDestroyTime >= TMNQDestroyTargetTime){
                DisableMNQ();
                TMNQSpawnTargetTime = Random.Range(GameManager.instance.mnqSpawnMinTime, GameManager.instance.mnqSpawnMaxTime);
                isSpawnMNQ = false;
            }
        }
        else if(isSpawnMNQ == false){
            TMNQSpawnTime += Time.deltaTime;
            if(TMNQSpawnTime >= TMNQSpawnTargetTime){
                SpawnMNQ();
            }
        }
    }
}

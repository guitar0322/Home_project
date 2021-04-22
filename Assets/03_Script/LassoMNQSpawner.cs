using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LassoMNQSpawner : Spawner
{
    public void StopMNQ(){
        for(int i = 0; i < spawnedObjectSet.childCount; i++){
            if (spawnedObjectSet.GetChild(i).gameObject.activeSelf)
            {
                spawnedObjectSet.GetChild(i).GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }
}

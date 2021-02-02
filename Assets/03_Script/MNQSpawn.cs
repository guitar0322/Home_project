using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNQSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform MNQSet;
    public Transform MNQPosSet;
    private GameObject SpawnedMNQ;
    int IsValidMNQ()
    {
        for(int i = 0; i < MNQSet.childCount; i++)
        {
            if (MNQSet.GetChild(i).gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }
    void SpawnMNQ(int num)
    {
        int MNQ_Idx, MNQ_Pos_Idx;
        for(int i = 0; i < num; i++)
        {
            MNQ_Idx = IsValidMNQ();
            MNQ_Pos_Idx = Random.Range(0, 4);
            SpawnedMNQ = MNQSet.GetChild(MNQ_Idx).gameObject;
            SpawnedMNQ.SetActive(true);
            SpawnedMNQ.transform.position = MNQPosSet.GetChild(MNQ_Pos_Idx).transform.position;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

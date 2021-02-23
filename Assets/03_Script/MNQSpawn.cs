using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform MNQSet;
    public Transform MNQPosSet;
    private GameObject SpawnedMNQ;
    private int IsValidMNQ()
    {
        for(int i = 0; i < MNQSet.childCount; i++)
        {
            if (MNQSet.GetChild(i).gameObject.activeSelf == false)
            {
                return i;
            }
        }
        return -1;
    }
    public void SpawnMNQ(int num)
    {
        bool setSpeedFlag;
        int MNQ_Idx, MNQ_Pos_Idx;
        setSpeedFlag = GameManager.instance.gameState < State.T_MNQ_THIRD ? false : true;
        for(int i = 0; i < num; i++)
        {
            MNQ_Idx = IsValidMNQ();
            if(MNQ_Idx == -1)
            {
                Debug.Log("invalid mnq num");
                return;
            }
            Debug.Log(MNQ_Idx);
            MNQ_Pos_Idx = Random.Range(0, MNQPosSet.transform.childCount);
            SpawnedMNQ = MNQSet.GetChild(MNQ_Idx).gameObject;
            SpawnedMNQ.GetComponent<TMNQ>().SetProperty(GameManager.instance.TMNQWaitMinTime, GameManager.instance.TMNQWaitMaxTime, setSpeedFlag);
            SpawnedMNQ.SetActive(true);
            SpawnedMNQ.transform.position = MNQPosSet.GetChild(MNQ_Pos_Idx).transform.position;
        }
    }

    public void DisableMNQ(int idx)
    {
        MNQSet.GetChild(idx).gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

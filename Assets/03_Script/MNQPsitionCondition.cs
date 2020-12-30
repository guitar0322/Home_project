using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQPsitionCondition : MonoBehaviour
{
    public bool isFlower = false;
    public GameObject mNQ_D;
    //public GameObject mNQ_F;
    public GameObject pianoFlower;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == mNQ_D)
        {
            GameManager.instance.gameState = State.O_MNQ_MOVE;
            pianoFlower.SetActive(true);
            this.enabled = false;
        }
    }  
}

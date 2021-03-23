﻿using System.Collections;
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
        if(GameManager.instance.gameState != State.O_DIARY_COMPLETE)
            return;
        if (other.gameObject == mNQ_D)
        {
            GameManager.instance.gameState++;
            pianoFlower.SetActive(true);
            this.enabled = false;
        }
        else if(other.tag.Equals("Player")){
            GameManager.instance.OMNQDropCondition = true;
        }
    }  
}

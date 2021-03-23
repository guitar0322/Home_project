using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQPositionCondition : MonoBehaviour
{
    public bool isFlower = false;
    public GameObject MNQ;
    //public GameObject mNQ_F;
    public GameObject pianoFlower;

    void OnTriggerEnter(Collider other)
    {
        if(GameManager.instance.gameState != State.O_DIARY_COMPLETE)
            return;
        if (other.gameObject == MNQ)
        {
            GameManager.instance.gameState++;
            pianoFlower.SetActive(true);
            this.enabled = false;
        }
        else if(other.tag.Equals("Player")){
            GameManager.instance.OMNQDropCondition = true;
        }
    }  

    public void SnapMNQ(){
        MNQ.transform.position = GameManager.instance.mnqSnapTransform.position;
        MNQ.transform.rotation = GameManager.instance.mnqSnapTransform.rotation;
    }
}

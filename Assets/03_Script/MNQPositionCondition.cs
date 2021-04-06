using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class MNQPositionCondition : MonoBehaviour
{
    public bool isFlower = false;
    public bool isSnap;
    public GameObject MNQ;
    //public GameObject mNQ_F;
    public GameObject pianoFlower;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player")){
            isSnap = true;
        }
    }  

    public void SnapMNQ(){
        if(isSnap == true && GameManager.instance.gameState == State.O_DIARY_COMPLETE){
            MNQ.transform.position = GameManager.instance.mnqSnapTransform.position;
            MNQ.transform.rotation = GameManager.instance.mnqSnapTransform.rotation;
            GameManager.instance.gameState++;
            pianoFlower.SetActive(true);
            this.enabled = false;
        }
    }
}

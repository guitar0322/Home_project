using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level_2_ColliderChecker : MonoBehaviour
{
    public bool isEnter;

    ObjectCheck objectCheck;
    SetMNQNewPosition setMNQNewPosition;
    MNQMoveControl mNQMoveControl;   

    private void Start()
    {
        objectCheck = GameObject.FindWithTag("Player").GetComponent<ObjectCheck>();
        setMNQNewPosition = GameObject.FindWithTag("SetMNQNewPosition").GetComponent<SetMNQNewPosition>();             
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MNQ") && objectCheck.isCanMNQMove)
        {
            mNQMoveControl = GameObject.Find(setMNQNewPosition.mNQ_D.name + "(Clone)").GetComponent<MNQMoveControl>();
            //Debug.Log("MNQ Move Stop");
            mNQMoveControl.nav.speed = 0f;
            //Debug.Log(mNQMoveControl.nav.speed);
            isEnter = true;
        }
    }
}

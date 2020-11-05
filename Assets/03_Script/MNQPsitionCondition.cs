using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            isFlower = true;
            pianoFlower.SetActive(true);
            Destroy(this.gameObject);
        }
    }  
}

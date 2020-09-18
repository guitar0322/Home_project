using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNQPsitionCondition : MonoBehaviour
{
    public bool isMNQ1 = false;
    GameObject mNQ;
    public GameObject door;

    void Awake()
    {
        mNQ = GameObject.FindGameObjectWithTag("MNQ");       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MNQ")
        {            
            isMNQ1 = true;
            door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

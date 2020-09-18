using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMNQNewPosition_DoorExit : MonoBehaviour
{
    public GameObject door;
    public GameObject mNQ01;
    public GameObject setMNQNewPosition01;

    void Start()
    {
        mNQ01 = GameObject.FindGameObjectWithTag("MNQ");
        setMNQNewPosition01 = GameObject.FindGameObjectWithTag("SetMNQNewPosition");
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {           
            mNQ01.transform.position = setMNQNewPosition01.transform.position;
            door.SetActive(true);
        }
    }
}
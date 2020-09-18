using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            door.SetActive(true);            
        }
    }
}

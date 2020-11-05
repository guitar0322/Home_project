using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject mNQ_D;
    public GameObject soju;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            mNQ_D.SetActive(false);
            soju.SetActive(true);
        }
    }
}

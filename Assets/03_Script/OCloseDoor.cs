﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCloseDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject O_MNQ;
    private void OnTriggerEnter(Collider other)
    {
        door.transform.eulerAngles = new Vector3(-90, 0, -90);
        O_MNQ.SetActive(false);
        Debug.Log("test");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

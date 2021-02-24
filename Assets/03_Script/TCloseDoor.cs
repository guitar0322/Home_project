using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class TCloseDoor : MonoBehaviour
{
    public GameObject rightDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.gameState == State.T_DOOR)
        {
            rightDoor.transform.localEulerAngles = new Vector3(-90, 0, 180);
        }
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

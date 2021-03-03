using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class CloseDoorTrigger : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public Transform TCloseDoor;
    public Transform TEndCloseDoor;
    public Transform THCloseDoor;

    public ObjectManager objectManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") == false)
            return;
        switch (GameManager.instance.gameState)
        {
            case State.O_DOOR:
                leftDoor.transform.localEulerAngles = new Vector3(-90, 0, -90);
                this.transform.position = TCloseDoor.position;
                this.transform.localEulerAngles = new Vector3(0, 90, 0);
                objectManager.mnq.SetActive(false);
                break;
            case State.T_DOOR:
                rightDoor.transform.localEulerAngles = new Vector3(-90, 0, 180);
                this.transform.position = TEndCloseDoor.position;
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case State.T_END_DOOR:
                rightDoor.transform.localEulerAngles = new Vector3(-90, 0, 180);
                objectManager.DisableTObject();
                Destroy(this);
                break;
        }
    }

    private void Start()
    {
        if(GameManager.instance.gameState > State.O_DOOR && GameManager.instance.gameState < State.T_DOOR)
        {
            this.transform.position = TCloseDoor.position;
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (GameManager.instance.gameState > State.T_DOOR && GameManager.instance.gameState < State.T_END_DOOR)
        {
            this.transform.position = TEndCloseDoor.position;
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}

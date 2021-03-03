using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class THTrigger : MonoBehaviour
{
    public ObjectManager objectManager;
    public Transform posSet;
    public Transform mnqPosSet;
    public GameObject leftDoor;
    int posIdx = 0;
    int mnqPosIdx = 0;
    private void OnTriggerEnter(Collider other)
    {
        switch (GameManager.instance.gameState)
        {
            case State.TH_START_DOOR:
                leftDoor.transform.localEulerAngles = new Vector3(-90, 0, -90);
                GameManager.instance.gameState++;
                objectManager.firstTrapWall.SetActive(true);
                break;
            case State.TH_DRUM:
                GameManager.instance.gameState++;
                objectManager.secondTrapWall.SetActive(true);
                break;
            case State.TH_MASK:
                GameManager.instance.gameState++;
                objectManager.thirdTrapWall.SetActive(true);
                break;
        }
    }

    public void Next()
    {
        this.transform.position = posSet.GetChild(posIdx).position;
        objectManager.THMNQ.transform.position = mnqPosSet.GetChild(mnqPosIdx).position;
        mnqPosIdx++;
        posIdx++;
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

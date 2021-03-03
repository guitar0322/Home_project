using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumbingoPuzzle : MonoBehaviour
{
    public GameObject caseBox;
    Vector3 mouseStart;
    Vector3 mouseNext;
    Vector3 center;
    Vector3 cross;
    bool moveFlag;
    public Transform pills;
    public int numOfPill;
    public ObjectManager objectManager;
    public THTrigger thTrigger;
    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(Screen.width/2, Screen.height/2, 0);
        numOfPill = pills.childCount;
    }

    public void Clear()
    {
        GameManager.instance.ExitUIMode();
        GameManager.instance.gameState++;
        objectManager.firstTrapWall.SetActive(false);
        thTrigger.Next();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveFlag = true;
            mouseStart = (Input.mousePosition - center).normalized;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveFlag = false;
        }

        if (moveFlag)
        {
            mouseNext = (Input.mousePosition - center).normalized;
            cross = Vector3.Cross(mouseStart, mouseNext);
            caseBox.transform.Rotate(cross * GameManager.instance.caseBoxSpeed);
            mouseStart = mouseNext;
        }
    }
}

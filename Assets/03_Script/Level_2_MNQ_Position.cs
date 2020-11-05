using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_MNQ_Position : MonoBehaviour
{
    public bool isPositionSoju;
    public bool isPositionFallenLeaves;
    public bool isPositionAcceptance_Mobile;

    public GameObject soju_Position;
    public GameObject fallenLeaves_Position;
    public GameObject acceptance_Mobile_Position;

    GameObject mNQ_D;
    ObjectCheck objectCheck;
    Level_2_MNQ_Position level_2_MNQ_Position;

    int i;

    private void Start()
    {
        mNQ_D = GameObject.FindWithTag("MNQ").GetComponent<GameObject>();
        objectCheck = GameObject.FindWithTag("Player").GetComponent<ObjectCheck>();
        level_2_MNQ_Position = GameObject.Find("Level_2_MNQ_Set(Need)_Position").GetComponent<Level_2_MNQ_Position>();
    }

    private void Update()
    {
        SojuEnter();
        FallenLeavesEnter();
        AcceptanceMobileEnter();
    }

    void SojuEnter()
    {        
        GameObject sojuPosition = level_2_MNQ_Position.transform.GetChild(0).gameObject;

        if(sojuPosition.GetComponent<Level_2_ColliderChecker>().isEnter)
        {
            isPositionSoju = true;
            //Debug.Log("Position Soju");
        }

    }

    void FallenLeavesEnter()
    {
        GameObject fallenLeavesPosition = level_2_MNQ_Position.transform.GetChild(1).gameObject;

        if (fallenLeavesPosition.GetComponent<Level_2_ColliderChecker>().isEnter)
        {
            isPositionFallenLeaves = true;
            //Debug.Log("Position Fallen");
        }
    }

    void AcceptanceMobileEnter()
    {
        GameObject acceptanceMobilePosition = level_2_MNQ_Position.transform.GetChild(2).gameObject;

        if (acceptanceMobilePosition.GetComponent<Level_2_ColliderChecker>().isEnter)
        {
            //함수
            isPositionAcceptance_Mobile = true;
        }
    }
}

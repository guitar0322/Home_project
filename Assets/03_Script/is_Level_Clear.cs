using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class is_Level_Clear : MonoBehaviour
{
    public bool isClear_1 = false;
    public GameObject door;

    MNQPsitionCondition mNQPsitionCondition;

    void Start()
    {
        mNQPsitionCondition = GameObject.FindGameObjectWithTag("MNQNeedPosition").GetComponent<MNQPsitionCondition>();
    }
    void Update()
    {
        IsClear_1();
    }

    void IsClear_1()
    {
        Vector3 mouseDownPos;
        Ray ray;
        RaycastHit hit;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);

        if (mNQPsitionCondition.isMNQ1 == true && Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "ClearTrigger")
            {
                isClear_1 = true;
                door.SetActive(false);
            }
        }
    }
}

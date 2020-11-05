using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class is_Level_Clear : MonoBehaviour
{
    public bool isClear_1 = false;
    public GameObject door;

    ObjectCheck objectCheck;

    void Start()
    {
        objectCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectCheck>();
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

        if (objectCheck.isInteract_PianoFlower == true && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "ClearTrigger")
            {
                isClear_1 = true;
                door.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    ObjectCheck objectCheck;

    Transform tr;

    public float speed;
    public float angle;

    public Vector3 dirention;

    public bool canObjectCheck;

    private void Start()
    {
        tr = GetComponent<Transform>();
        objectCheck = GameObject.FindWithTag("Player").GetComponent<ObjectCheck>();
        canObjectCheck = false;
    }

    private void Update()
    {
        if (objectCheck.isInteract_PianoFlower && gameObject.name == "Door01Hinge" && !canObjectCheck)
        {
            tr.eulerAngles = new Vector3(0, -30, 0);
            canObjectCheck = true;
        }
    }
}

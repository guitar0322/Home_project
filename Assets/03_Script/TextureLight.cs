using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLight : MonoBehaviour
{
    public GameObject maskObject;
    Vector3 mouseUpPos;
    Ray ray;
    RaycastHit raycastHit;
    void Start()
    {
        
    }

    void Update()
    {
        mouseUpPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseUpPos);
        Debug.DrawRay(ray.origin, ray.direction , Color.black);
        if (Physics.Raycast(ray, out raycastHit, 5f))
        {
            maskObject.transform.position = raycastHit.point;
            maskObject.SetActive(true);
        }
        else{
            maskObject.SetActive(false);
        }
    }
}

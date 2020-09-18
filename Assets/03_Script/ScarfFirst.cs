using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfFirst : MonoBehaviour
{
    public GameObject scarf;
    
    public bool mNQScarf = false;

   
    void Update()
    {
        ScarfSetActive();
    }

    void ScarfSetActive()
    {
        Vector3 mouseDownPos;
        Ray ray;
        RaycastHit hit;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        DiaryOpen diaryOpen = GameObject.Find("Diary").GetComponent<DiaryOpen>();

        if(diaryOpen.fishScarf == true)
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit,1f) && hit.collider.name == "Scarf")
            {
                scarf.SetActive(false);
                mNQScarf = true;              
                
                Debug.Log("MoveMNQ02 Clear");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNQ_Follow_Trigger : MonoBehaviour
{
    //MNQFollow활성화 코드

    public bool isMNQFollow = false;
    is_Level_Clear level_Clear;

    private void Start()
    {
        level_Clear = GameObject.FindGameObjectWithTag("ClearTrigger").GetComponent<is_Level_Clear>();
    }

    private void Update()
    {
        ISMNQFollow();
    }

    void ISMNQFollow()
    {
        Vector3 mouseDownPos;
        Ray ray;
        RaycastHit hit;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);

        if (level_Clear.isClear_1 == true && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "MNQ_Follow_Trigger")
            {
                isMNQFollow = true;
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBreadFirst : MonoBehaviour
{
    public GameObject fishBread;   
      
    public bool mMNQFish = false;

  
    void Update()
    {
        FBSetActive();
    }
    void FBSetActive()
    {
        Vector3 mouseDownPos;
        Ray ray;
        RaycastHit hit;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        DiaryOpen diaryOpen = GameObject.Find("Diary").GetComponent<DiaryOpen>();

        //붕어빵, 목도리 상호작용 가능하게 된다면, 그게 Ray하고 충돌이 된다면 그리고 충돌된 collider의 이름이 FishBread라면
        if (diaryOpen.fishScarf == true && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 1f) && hit.collider.name == "FishBread")                       
        {
            fishBread.SetActive(false);              //FishBread를 비활성화 시킨다.
            
            mMNQFish = true;                         //MNQ를 움직일 조건1 달성
            Debug.Log("MoveMNQ01 Clear");   
        }
        
    }

    
}

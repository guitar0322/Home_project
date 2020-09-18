using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    AudioSource diaryDropSound;

    public float m_RayDistance =0f;
    public bool diaryMax01=true;
    
    void Start()
    {
        diaryDropSound = transform.GetChild(2).GetComponent<AudioSource>(); // 자식 2 = DropSound
    }

    void Update()
    {
        ObjectCheckByRay();
    }
    private void ObjectCheckByRay()
    {
        Vector3 mouseDownPos;
        RaycastHit hit;
        Ray ray;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        if (Input.GetMouseButtonDown(0))
        { 
            if (diaryMax01)
            {
                if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "MNQ")
                {
                    Diary01();
                    diaryMax01 = false;
                    Debug.Log("DiaryContent Can Open & Close");
                }     
            }
        }
    }
    private void Diary01()
    {        
        GameObject diary = GameObject.FindWithTag("Diary");         

        diary.transform.Translate(new Vector3(-1f, -0.2f, -0.3f));
        diary.transform.Rotate(0, 0, 90);
        diaryDropSound.Play();
    }
}

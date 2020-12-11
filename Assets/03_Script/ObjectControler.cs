using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControler : MonoBehaviour
{
    private float rotSpeed = 3;
    public GameObject camArm;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotCtrl();
    }

    void RotCtrl()
    { //마우스 회전 시점이동 함수
        Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
        Cursor.visible = false;//마우스 커서 보이기            

        float rotVer = Input.GetAxis("Mouse Y") * rotSpeed;   // 마우스 회전
        float rotHor = Input.GetAxis("Mouse X") * rotSpeed;   // 마우스 회전
        //camArm.transform.localRotation *= Quaternion.Euler(-rotVer, -rotHor, 0);           // 마우스 회전
        camArm.transform.Rotate(Vector3.forward, rotVer);
        camArm.transform.Rotate(Vector3.up, rotHor);


        //fpsCam.transform.rotation *= Quaternion.Euler(-rotVer, 0, 0);    // 마우스 회전
    }
}

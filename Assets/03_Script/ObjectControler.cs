using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControler : MonoBehaviour
{
    private float rotSpeed = 3;             //오브젝트 회전 속도
    private GameObject targetObj;           //상호작용 오브젝트
    private Vector3 targetPosition;         //오브젝트가 이동할 카메라의 중심위치
    private bool closeUpFlag;               //오브젝트 이동을 알리는 플래그
    private bool ctrlFlag;
    private float moveTimeInSeconds = 1f;   //오브젝트 이동속도
    private Vector3 oldPosition;
    private Quaternion oldRotate;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotCtrl();
        if(closeUpFlag == true)
        {
            if (Vector3.Distance(targetObj.transform.position, targetPosition) < 0.01f)
            {
                ctrlFlag = true;
            }
            else
            {
                targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, targetPosition, moveTimeInSeconds * Time.deltaTime);
                Debug.Log("target : " + targetObj.transform.position + " old : " + oldPosition);

            }
        }
        else if(closeUpFlag == false)
        {
            Debug.Log("recover");
            Debug.Log("target : " + targetObj.transform.position + " old : " + oldPosition);
            if (Vector3.Distance(targetObj.transform.position, oldPosition) < 0.01f)
            {
                ctrlFlag = false;
                this.enabled = false;
            }
            else
            {
                targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, oldPosition, moveTimeInSeconds * Time.deltaTime);

                //Vector3 targetDir = oldRotate - targetObj.transform.position;
                float step = moveTimeInSeconds * Time.deltaTime * 500;
                targetObj.transform.rotation = Quaternion.RotateTowards(targetObj.transform.rotation, oldRotate, step);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            closeUpFlag = false;
        }

    }

    public void SetProperty(GameObject obj, Vector3 position)
    {
        //오브젝트 이동을 위한 클래스의 속성값 SET.
        oldPosition = obj.transform.position;
        oldRotate = obj.transform.rotation;
        targetObj = obj;
        targetPosition = position;
        closeUpFlag = true;
    }

    void RotCtrl()
    { 
        if(ctrlFlag == true)
        {
            Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
            Cursor.visible = false;//마우스 커서 보이기            

            float rotVer = Input.GetAxis("Mouse Y") * rotSpeed;   // 수직회전
            float rotHor = Input.GetAxis("Mouse X") * rotSpeed;   // 수평회전
            targetObj.transform.Rotate(Vector3.forward, rotVer, Space.World);
            targetObj.transform.Rotate(Vector3.up, rotHor, Space.World);

        }
    }
}

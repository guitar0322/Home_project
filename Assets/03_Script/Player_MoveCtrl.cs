using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveCtrl : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed = 3.0f;
    public float backMove = 0.7f;

    public GameObject fpsCam;
    GameObject playerEquipPoint;    
    bool  isEquip = false;
    public bool isSee = false;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
    }
    
    void Update()
    {
        RotCtrl();  // 마우스 포인터를 통한 시점 변화
    }

    void FixedUpdate()
    {
        MoveCtrl(); // 캐릭터 움직임
    }
    public void MoveCtrl()
    { //키보드 W,S,A,D Player 이동
        float hor = Input.GetAxis("Horizontal") * moveSpeed;
        float ver = Input.GetAxis("Vertical") * moveSpeed;


        Vector3 pos = transform.forward * Time.fixedDeltaTime * ver + transform.right * Time.fixedDeltaTime * hor;

        pos += transform.position;

        rigidbody.MovePosition(pos);
        //if (Input.GetKey(KeyCode.W))
        //{
        //    this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    this.transform.Translate(Vector3.back * moveSpeed * backMove * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        //}
        if (Input.GetMouseButtonDown(1) && isEquip == true)
        {
            Drop();
        }
    }

    public void PickUp(GameObject item)
    {
        isEquip = true;
        SetEquip(item, true);
    }
    void Drop()
    {
        GameObject item = playerEquipPoint.GetComponentInChildren<Rigidbody>().gameObject;
        SetEquip(item, false);        
        playerEquipPoint.transform.DetachChildren();
        isEquip = false;
    }

    void SetEquip(GameObject item, bool isEquip)
    {
        Collider[] itemColliders = item.GetComponents<Collider>();
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

        foreach (Collider itemCollider in itemColliders)
        {
            itemCollider.enabled = !isEquip;
        }
        itemRigidbody.isKinematic = isEquip;
    }

    void RotCtrl()
    { //마우스 회전 시점이동 함수
        Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
        Cursor.visible = false;//마우스 커서 보이기            

        float rotHor = Input.GetAxis("Mouse X") * rotSpeed;   // 마우스 회전
        float rotVer = Input.GetAxis("Mouse Y") * rotSpeed;   // 마우스 회전
        Debug.Log(rotHor + " , " + rotVer);

        this.transform.localRotation *= Quaternion.Euler(0, rotHor, 0);           // 마우스 회전
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotVer, 0, 0);    // 마우스 회전

        if (fpsCam.transform.localRotation.eulerAngles.x > 0 && fpsCam.transform.localRotation.eulerAngles.x < 180f)
        {
            fpsCam.transform.localRotation = Quaternion.Euler((Mathf.Clamp(fpsCam.transform.rotation.eulerAngles.x, 0, 65f)), 0f, 0f);
        }

        if (fpsCam.transform.localRotation.eulerAngles.x > 180 && fpsCam.transform.localRotation.eulerAngles.x < 360)
            fpsCam.transform.localRotation = Quaternion.Euler((Mathf.Clamp(fpsCam.transform.rotation.eulerAngles.x, 290, 360f)), 0f, 0f);
    }
   
}

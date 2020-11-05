using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveCtrl : MonoBehaviour
{    
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;
    public float backMove = 0.7f;

    public Camera fpsCam;
    GameObject playerEquipPoint;    
    bool  isEquip = false;
    public bool isSee = false;

    Transform myTr;

    void Start()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        myTr = GetComponent<Transform>();
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
    }
    
    void Update()
    {
        MoveCtrl(); // 캐릭터 움직임
        RotCtrl();  // 마우스 포인터를 통한 시점 변화
    }
   
    public void MoveCtrl()
    { //키보드 W,S,A,D Player 이동
        if (Input.GetKey(KeyCode.W))
        {
            this.myTr.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.myTr.Translate(Vector3.back * moveSpeed * backMove * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.myTr.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.myTr.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
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

        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;   // 마우스 회전
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;   // 마우스 회전

        this.myTr.localRotation *= Quaternion.Euler(0, rotY, 0);           // 마우스 회전
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);    // 마우스 회전
    }
   
}

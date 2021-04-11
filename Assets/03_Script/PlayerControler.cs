using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Vector3 mouseUpPos;
    Ray ray;
    RaycastHit raycastHitObject;

    public float m_RayDistance = 1f;
    public float moveSpeed;
    public float rotSpeed = 3.0f;
    public float backMove = 0.7f;
    public bool moveControlFlag;
    public bool rotateControlFlag;
    public bool raycastControlFlag;

    public GameObject fpsCam;
    public bool isEquip = false;
    Rigidbody rigidbody;

    [Header("Component")]
    public ObjectManager objectManager;
    public ViewMode viewMode;

    [Header("Debug")]
    public GameObject equipPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        moveControlFlag = true;
        rotateControlFlag = true;
        raycastControlFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotCtrl();
        RaycastCheck();
        if(Input.GetMouseButtonUp(0))
            PressMouseLHandler();
        if (Input.GetMouseButtonUp(1))
            PressMouseRHandler();
        PressKeyBoardHandler();
    }
    void FixedUpdate()
    {
        MoveCtrl(); // 캐릭터 움직임
    }

    void RaycastCheck()
    {
        if(raycastControlFlag == false)
            return;
        mouseUpPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseUpPos);
        Debug.DrawRay(ray.origin, ray.direction , Color.red);
        if (Physics.Raycast(ray, out raycastHitObject, m_RayDistance))
        {
            GameManager.instance.CheckFKeyUI(raycastHitObject.collider.gameObject);
        }
    }
    void PressKeyBoardHandler()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            viewMode.TakeViewModeObject();
            objectManager.InteractMNQ();
        }
    }


    void PressMouseLHandler()
    {
        if (Physics.Raycast(ray, out raycastHitObject, m_RayDistance))
        {
            objectManager.InteractionObject(raycastHitObject);
        }
        else
        {
            Debug.Log("raycastHit fail in playerControler");
        }
    }
    void PressMouseRHandler()
    {
        viewMode.ViewModeExit();
        GameManager.instance.ExitUIMode();
        if(isEquip == true)
            DropItem();
    }

    public void MoveCtrl()
    { //키보드 W,S,A,D Player 이동
        if (moveControlFlag == false)
            return;
        float hor = Input.GetAxis("Horizontal") * moveSpeed * GameManager.instance.slowWeight;
        float ver = Input.GetAxis("Vertical") * moveSpeed * GameManager.instance.slowWeight; 

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
    }

    public void EquipItem(GameObject item, Vector3 equipPosition)
    {
        isEquip = true;
        item.transform.SetParent(equipPoint.transform);
        item.transform.localPosition = equipPosition;
        item.transform.localEulerAngles = Vector3.zero;
        item.layer = LayerMask.NameToLayer("UI");
        SetEquip(item, true);
    }
    void DropItem()
    {
        GameObject item = equipPoint.GetComponentInChildren<Rigidbody>().gameObject;
        if (item.tag.Equals("Candle"))
        {
            return;
        }
        if(item.tag.Equals("MNQ")){
            if(objectManager.OMNQTrigger.isSnap == true){
                Debug.Log("test");
                isEquip = false;
                SetEquip(item, false);
                item.layer = LayerMask.NameToLayer("Default");
                equipPoint.transform.DetachChildren();
                objectManager.OMNQTrigger.SnapMNQ();
            }
        }
    }

    void SetEquip(GameObject item, bool isEquip)
    {
        foreach (Collider itemCollider in item.GetComponents<Collider>())
        {
            itemCollider.enabled = !isEquip;
        }
        item.GetComponent<Rigidbody>().isKinematic = isEquip;
    }

    void RotCtrl()
    {
        if (rotateControlFlag == false)
            return;
        //마우스 회전 시점이동 함수
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        

        float rotHor = Input.GetAxis("Mouse X") * rotSpeed * GameManager.instance.slowWeight;   // 마우스 회전
        float rotVer = Input.GetAxis("Mouse Y") * rotSpeed * GameManager.instance.slowWeight;   // 마우스 회전

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

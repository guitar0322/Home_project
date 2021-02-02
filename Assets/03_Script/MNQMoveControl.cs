using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNQMoveControl : MonoBehaviour
{    
    bool isPlayerEnter;
    public bool isMove;
           
    public GameObject player;
    GameObject playerEquipPoint;
    
    public NavMeshAgent nav;
    Rigidbody rigid;

    Player_MoveCtrl moveCtrl;
    ObjectCheck objectCheck;
    //MNQ_Follow_Trigger mNQ_Follow_Trigger;
    is_Level_Clear level_Clear;
    Level_2_MNQ_Position level_2_MNQ_Position;

    void Awake()
    {
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
        moveCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_MoveCtrl>();        
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        //mNQ_Follow_Trigger = GameObject.FindGameObjectWithTag("MNQ_Follow_Trigger").GetComponent<MNQ_Follow_Trigger>();
        level_Clear = GameObject.FindGameObjectWithTag("ClearTrigger").GetComponent<is_Level_Clear>();
        objectCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectCheck>();
        level_2_MNQ_Position = GameObject.FindWithTag("Level_2_MNQ_Position").GetComponent<Level_2_MNQ_Position>();
    }

    void Update() 
    {
        MNQEqupiment();
    }

    void MNQEqupiment()
    {

        Vector3 mouseDownPos;
        mouseDownPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseDownPos);

        if (level_Clear.isClear_1 == false)
        {
            if (objectCheck.mNQMove == true && Input.GetMouseButtonDown(0) && isPlayerEnter)
            {
                transform.SetParent(playerEquipPoint.transform);
                transform.localPosition = Vector3.zero;
                transform.rotation = new Quaternion(0, 0, 0, 0);

                moveCtrl.PickUp(gameObject);
                isPlayerEnter = false;
            }
        }
        else
        {

        }
        
    }

    // 콜리아더 충돌 시, MNQ 정지
    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void OnCollisionEnter(Collision collision) // 장착
    {
        if (collision.gameObject == player)
        {
            isPlayerEnter = true;
        }              
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = true;
        }
    }*/

    void OnCollisionExit(Collision collision) // 장착
    {
        if (collision.gameObject == player)
        {
            isPlayerEnter = false;
        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
        }
    }*/

    // 유저 추적
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("MNQ_Follow_Player", objectCheck.randomTime);       
        }
    }

    void MNQ_Follow_Player()
    {
        Debug.Log("Follow!");

        if(objectCheck.isCanMNQMove && !level_2_MNQ_Position.isPositionAcceptance_Mobile)
        {
            nav.SetDestination(player.transform.position);            
        }

        //if((level_2_MNQ_Position.isPositionSoju || level_2_MNQ_Position.isPositionFallenLeaves) && isMove)
        //{
        //    nav.speed = 0;            
        //}

        //if (!isMove)
        //{
        //    nav.speed = 1.5f;
        //}
    }
}

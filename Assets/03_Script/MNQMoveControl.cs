using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNQMoveControl : MonoBehaviour
{
    public Transform target;

    bool isPlayerEnter;
       
    GameObject player;
    GameObject playerEquipPoint;
    
    NavMeshAgent nav;
    Rigidbody rigid;

    Player_MoveCtrl moveCtrl;
    DiaryOpen diaryOpen;
    MNQ_Follow_Trigger mNQ_Follow_Trigger;
    is_Level_Clear level_Clear;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
        moveCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_MoveCtrl>();
        diaryOpen = GameObject.FindGameObjectWithTag("Diary").GetComponent<DiaryOpen>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        mNQ_Follow_Trigger = GameObject.FindGameObjectWithTag("MNQ_Follow_Trigger").GetComponent<MNQ_Follow_Trigger>();
        level_Clear = GameObject.FindGameObjectWithTag("ClearTrigger").GetComponent<is_Level_Clear>();
    }

    void Update() 
    {
        MNQEqupiment();
    }

    void MNQEqupiment()
    {
        if(level_Clear.isClear_1 == false)
        {
            if (diaryOpen.mNQMove == true && Input.GetMouseButtonDown(0) && isPlayerEnter)
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
        if (mNQ_Follow_Trigger.isMNQFollow == true && other.tag == "Player")
        {
            Debug.Log("Follow!");
            nav.SetDestination(target.position);           
        }
    }
}

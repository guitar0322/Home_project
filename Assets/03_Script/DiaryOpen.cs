using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryOpen : MonoBehaviour
{    
    public FishBreadFirst fishBreadFirst;
    public ScarfFirst scarfFirst;
    public Player_MoveCtrl player_MoveCtrl;
    public GameObject book_paper;
    AudioSource diaryOpenSound;
    AudioSource diaryFilpSound;

    public bool mNQMove = false;
    public bool fishScarf = false;

    public void Awake()
    {        
        Book book_paper = GetComponent<Book>();
        diaryOpenSound = transform.GetChild(0).GetComponent<AudioSource>(); // 자식 0 = OpenSound
        diaryFilpSound = transform.GetChild(1).GetComponent<AudioSource>(); // 자식 1 = FilpSound        
    }

    void Update()
    {
        DiaryCheck();
    }
    void DiaryCheck()
    {
        Vector3 mouseDownPos;
        Ray ray;
        RaycastHit hit;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        GameObject DiaryCon01 = GameObject.FindWithTag("Diary");
        ObjectCheck objectCheck = GameObject.Find("Player").GetComponent<ObjectCheck>();
        

        if (objectCheck.diaryMax01 == false) //MNQ를 한 번이라도 눌렀는가? Diary를 켜기 위한 조건
        {            
            if (Input.GetMouseButtonDown(0)) //Diary 켜는 방법
            {
                if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "Diary") //Ray의 충돌이 있다고 충돌된 콜라이더의 태그가 Diary 라면
                {
                    player_MoveCtrl.enabled = false; //유저 움직임 멈춤
                    Cursor.visible = true; // 마우스 보임 
                    Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
                    book_paper.SetActive(true); // 일기장 보이기, ######일기장 UI는 짝수여야 제대로 작동함#####.
                    diaryOpenSound.Play(); // 일기장 소리 열기

                    fishScarf = true;                       // 붕어빵, 목도리하고 상호작용 가능하게 한다.
                    Debug.Log("Diary Open");
                }                
            }
            //FishScarf 이후
            if (fishBreadFirst.mMNQFish == true && scarfFirst.mNQScarf == true) //마네킹을 이동시킬 수 있는 조건 달성으로 DiaryContent02보여주기.
            {               
                if (Input.GetMouseButtonDown(0)) //Diary 켜는 방법
                {
                    if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "Diary")        //Ray의 충돌이 있다고 충돌된 콜라이더의 태그가 Diary 라면
                    {   
                        
                        mNQMove = true; //마네킹 움직이는 조건 달성
                        Debug.Log("Diary02 Open");
                    }
                }
            }
            if (Input.GetMouseButtonDown(1)) //Diary를 닫는 방법
            {
                player_MoveCtrl.enabled = true; // 유저 다시 움직인다.
                book_paper.SetActive(false); // 일기장 숨기기
                Debug.Log("Diary Close");
            }            
        }
    }

    public void FlipSound()
    {
        diaryFilpSound.Play();
    }    
}

            
    


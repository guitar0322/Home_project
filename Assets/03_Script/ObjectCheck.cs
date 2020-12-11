using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectCheck : MonoBehaviour
{
    public float m_RayDistance = 100f;
    public bool diaryMax01 = true;

    public bool mNQMove = false;
    public bool can_Interact_Level1_Objects = false;


    public bool viewMode = false;
    public bool uiMode = false;

    public GameObject mNQ_DPrefabs;
    public GameObject book_paper;

    //###### 상호작용 오브젝트들 초기화 칸 ###################
    public GameObject scarf;
    public bool isInteract_Scarf = false;

    public GameObject fishBread;
    public bool isInteract_FishBread = false;

    public GameObject medicine_Envelope_A;
    public bool isInteract_Medicine_Envelope_A;

    public GameObject medicine_Envelope_B;
    public bool isInteract_Medicine_Envelope_B;

    public GameObject medicalSchool_Pic;
    public bool isInteract_MedicalSchool_Pic;
    
    public GameObject medicalSchool_AcceptanceLetter;
    public bool isInteract_MedicalSchool_AcceptanceLetter;
    //############ 일기장 이후 오브젝트 #######################
    public GameObject pianoFlower;
    public bool isInteract_PianoFlower;

    public GameObject level_1_Door01;

    public GameObject mirror;
    public GameObject Cracking_mirror;
    //#########################################################

    //################### Level 02 ############################
    public bool isCanMNQMove = false;

    public GameObject soju;
    public GameObject position_Soju;
    bool haveSoju = false;

    public GameObject fallenLeaves;
    public GameObject Position_fallenLeaves;
    bool haveFallenLeaves = false;

    public GameObject acceptance_Mobile;
    public GameObject Position_acceptance_Mobile;
    bool haveAcceptance_Mobile = false;

    public int randomInt_Min_Time = 1;
    public int randomInt_Max_Time = 5;
    public int randomTime;

    NavMeshAgent navMNQ;
    //#########################################################

    AudioSource diaryDropSound;
    AudioSource diaryOpenSound;
    AudioSource diaryFilpSound;
    AudioSource mirrorCrackingSound;
    AudioSource pianoBGM;

    Player_MoveCtrl playerControler;
    ObjectControler objectControler;

    MNQPsitionCondition mNQPsition;
    OKMeshChange oKMesh;
    SetMNQNewPosition setMNQNewPosition;
    Level_2_MNQ_Position level_2_MNQ_Position;
    DoorHinge doorHinge;

    RaycastHit hit;
    Ray ray;

    public GameObject viewModeTargetObj;
    public GameObject viewModeCamArm;
    public GameObject UI;

    public GameObject rawImage;

    public void FlipSound()
    {
        diaryFilpSound.Play();
    }

    void Awake()
    {
        Book book_paper = GetComponent<Book>();
        playerControler = GameObject.FindWithTag("Player").GetComponent<Player_MoveCtrl>();
        objectControler = GetComponent<ObjectControler>();

        mNQPsition = GameObject.FindWithTag("MNQNeedPosition").GetComponent<MNQPsitionCondition>();
        oKMesh = GameObject.Find("RenderChangeManager").GetComponent<OKMeshChange>();
        setMNQNewPosition = GameObject.FindWithTag("SetMNQNewPosition").GetComponent<SetMNQNewPosition>();
        level_2_MNQ_Position = GameObject.Find("Level_2_MNQ_Set(Need)_Position").GetComponent<Level_2_MNQ_Position>();
        doorHinge = GameObject.FindWithTag("DoorHinge").GetComponent<DoorHinge>();

        diaryOpenSound = GameObject.Find("DiaryOpenSound").GetComponent<AudioSource>(); // 자식 0 = OpenSound
        diaryFilpSound = GameObject.Find("DiaryFilpSound").GetComponent<AudioSource>(); // 자식 1 = FilpSound 

        mirrorCrackingSound = GameObject.Find("CrackingSound").GetComponent<AudioSource>();
        pianoBGM = GameObject.Find("PianoBGM").GetComponent<AudioSource>();


    }

    void Start()
    {
        diaryDropSound = transform.GetChild(2).GetComponent<AudioSource>(); // 자식 2 = DropSound        
    }

    void Update()
    {
        ObjectCheckByRay();
        if (Input.GetMouseButtonDown(1))
        {
            if (viewMode == true)
            {
                ViewModeExit();
            }
            else if (uiMode == true)
            {
                UIModeExit();
            }
        }
    }
    void InitViewMode(GameObject target)
    {
        Vector3 targetPos = Camera.main.WorldToScreenPoint(target.transform.position);
        viewModeTargetObj = Instantiate(target, targetPos, Quaternion.identity) as GameObject;
        viewModeTargetObj.transform.parent = viewModeCamArm.transform;
        //viewModeTargetObj.transform.localPosition = Vector3.zero;
        viewModeTargetObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        objectControler.enabled = true;
        playerControler.enabled = false;
        viewMode = true;
        rawImage.SetActive(true);
    }
    void InitUIMode()
    {
        uiMode = true;
        playerControler.enabled = false; //유저 움직임 멈춤

        Cursor.visible = true; // 마우스 보임 
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
        UI.SetActive(true); // UI 보이기
    }

    private void ViewModeExit()
    {
        playerControler.enabled = true; // 유저 다시 움직인다.
        objectControler.enabled = false;

        rawImage.SetActive(false); // 렌더텍스처 없애기
        viewMode = false;
        Destroy(viewModeTargetObj);
        viewModeTargetObj = null;
        Debug.Log("Diary Close");
    }

    private void UIModeExit()
    {
        playerControler.enabled = true;

        UI.SetActive(false); //UI 숨기기
        uiMode = false;
        Debug.Log("Diary Close");
    }

    private void ObjectCheckByRay()
    {
        if (viewMode || uiMode)
            return;
        Vector3 mouseDownPos;        
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        Debug.DrawRay(ray.origin, ray.direction * m_RayDistance, Color.red);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, m_RayDistance))
        {
            switch (hit.collider.tag)
            {
                case "MNQ":
                    FirstMNQ_Interactive();//첫 마네킹 상호작용
                    break;
                case "Diary":
                    Diary_Interactive();//일기장 함수
                    break;
                case "Scarf":
                    Scarf_Interactive();//목돌이 함수
                    break;
                case "FishBread":
                    FishBread_Interactive();                        // 붕어빵 함수
                    break;
                case "Medicine_Envelope_A":
                    Medicine_Envelope_A_Interactive();              // 약봉투 A 함수
                    break;
                case "Medicine_Envelope_B":
                    Medicine_Envelope_B_Interactive();              // 약봉투 B 함수
                    break;
                case "MedicalSchool_Pic":
                    MedicalSchool_Pic_Interactive();                // 의과대 사진 함수
                    break;
                case "MedicalSchool_AcceptanceLetter":
                    MedicalSchool_AcceptanceLetter_Interactive();   // 의과대 합격장 함수
                    break;
                case "Flower":
                    PianoFlower_Interactive();                      // 피아노 꽃 함수
                    break;
                case "Door":
                    Door_Interactive();                             // 문 열고 닫기 함수
                    break;
                case "Soju":
                    Soju_Interactive();                             // 소주 함수
                    break;
                case "FallenLeaves":
                    FallenLeaves_Interactive();                     // 낙엽 함수
                    break;
                case "Acceptance_Mobile":
                    Acceptance_Mobile_Interactive();                // 모바일 합격장 함수            
                    break;
                default:
                    InitViewMode(hit.transform.gameObject);
                    break;

            }
            Debug.Log(hit.collider.tag);
        }// 개별로 만든 이유는 나중에 각 오브젝트 별로 특별한 사운드 및 연출을 하기 위함.
    }

    void Diary_Interactive() //일기장 상호작용
    {

        Debug.Log("Diary Open");
        if (GameManager.instance.gameState == 1)
        {
            InitUIMode();
            playerControler.enabled = false;        //유저 움직임 멈춤
            Cursor.visible = true;                  // 마우스 보임
            Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
            book_paper.SetActive(true);     // 일기장 보이기, ######일기장 UI는 짝수여야 제대로 작동함#####.

            can_Interact_Level1_Objects = true;     // 붕어빵, 목도리하고 상호작용 가능하게 한다.
            Debug.Log("Diary Open");
        }

        if (isInteract_FishBread && isInteract_Scarf && isInteract_Medicine_Envelope_A &&
            isInteract_Medicine_Envelope_B && isInteract_MedicalSchool_Pic &&
            isInteract_MedicalSchool_AcceptanceLetter)  // 모든 필요 오브젝트 상호작용을 하면
        {                                               // 마네킹 상호작용 가능
            mNQMove = true;
            //Debug.Log("Can MNQ Move!"); 
        }
    }
    void FirstMNQ_Interactive() //마네킹 1회 이상 상호작용 하기
    {
        if (GameManager.instance.gameState == 0)
        {
            DropDiary();
            GameManager.instance.gameState++;
            //Debug.Log("DiaryContent Can Open & Close");                
        }

        if (haveSoju && level_2_MNQ_Position.isPositionSoju)
        {
            //Debug.Log("SOJUMNQQQQ");
            fallenLeaves.SetActive(true);
            haveSoju = false;
            Destroy(GameObject.Find(setMNQNewPosition.mNQ_D.name + "(Clone)"), 2); // 생성된 오브젝트 삭제
            Position_fallenLeaves.SetActive(true);
            //Destroy(level_2_MNQ_Position.soju_Position, 2.5f);
            level_2_MNQ_Position.soju_Position.SetActive(false);
        }
        if (haveFallenLeaves && level_2_MNQ_Position.isPositionFallenLeaves)
        {
            //Debug.Log("FallllenenenenenLeaves");
            acceptance_Mobile.SetActive(true);
            haveFallenLeaves = false;
            Destroy(GameObject.Find(setMNQNewPosition.mNQ_D.name + "(Clone)"), 2); // 생성된 오브젝트 삭제
            Position_acceptance_Mobile.SetActive(true);
            //Destroy(level_2_MNQ_Position.fallenLeaves_Position, 2.5f);
            level_2_MNQ_Position.fallenLeaves_Position.SetActive(false);
        }
        if (haveAcceptance_Mobile && level_2_MNQ_Position.isPositionAcceptance_Mobile)
        {
            haveAcceptance_Mobile = false;
            Destroy(GameObject.Find(setMNQNewPosition.mNQ_D.name + "(Clone)"), 1); // 생성된 오브젝트 삭제
            playerControler.enabled = false;
            Invoke("Cant_Playermove", 1);
            setMNQNewPosition.MobileAfterInteractive();
        }
    }

    private void DropDiary() // 일기장 떨어지는 함수
    {
        GameObject diary = GameObject.FindWithTag("Diary");

        diary.transform.localPosition = new Vector3(-3.422f, 1.446f, 5.548f);
        diary.transform.Rotate(0, 0, 90);
        diaryDropSound.Play();
    }


    void Scarf_Interactive() //목도리 상호작용 함수
    {
        if(can_Interact_Level1_Objects == true)
        {
            if(hit.collider.tag == "Scarf")
            {
                scarf.SetActive(false);
                isInteract_Scarf = true;
                
               //Debug.Log("Scarf Clear");
            }
        }
    }

    void FishBread_Interactive() //붕어빵 상호작용 함수
    {
        if(can_Interact_Level1_Objects == true)
        {
            if(hit.collider.tag == "FishBread")
            {
                fishBread.SetActive(false);
                isInteract_FishBread = true;

                //Debug.Log("FishBread Clear");
            }
        }
    }

    void Medicine_Envelope_A_Interactive() //약 봉투A 상호작용 함수
    {
        if (can_Interact_Level1_Objects == true)
        {
            if (hit.collider.tag == "Medicine_Envelope_A")
            {
                medicine_Envelope_A.SetActive(false);
                isInteract_Medicine_Envelope_A = true;

                //Debug.Log("Medicine_Envelope_A Clear");
            }
        }
    }

    void Medicine_Envelope_B_Interactive() //약 봉투A 상호작용 함수
    {
        if (can_Interact_Level1_Objects == true)
        {
            if (hit.collider.tag == "Medicine_Envelope_B")
            {
                medicine_Envelope_B.SetActive(false);
                isInteract_Medicine_Envelope_B = true;

                //Debug.Log("Medicine_Envelope_B Clear");
            }
        }
    }

    void MedicalSchool_Pic_Interactive() //약 봉투A 상호작용 함수
    {
        if (can_Interact_Level1_Objects == true)
        {
            if (hit.collider.tag == "MedicalSchool_Pic")
            {
                medicalSchool_Pic.SetActive(false);
                isInteract_MedicalSchool_Pic = true;

                //Debug.Log("MedicalSchool_Pic Clear");
            }
        }
    }

    void MedicalSchool_AcceptanceLetter_Interactive() //약 봉투A 상호작용 함수
    {
        if (can_Interact_Level1_Objects == true)
        {
            if (hit.collider.tag == "MedicalSchool_AcceptanceLetter")
            {
                medicalSchool_AcceptanceLetter.SetActive(false);
                isInteract_MedicalSchool_AcceptanceLetter = true;
                
                //Debug.Log("MedicalSchool_AcceptanceLetter Clear");
            }
        }
    }

    void PianoFlower_Interactive() //피아노꽃 함수
    {
        if (mNQPsition.isFlower == true)
        {
            if(hit.collider.tag == "Flower")
            {
                mirrorCrackingSound.Play();
                
                Invoke("PianoBGM_Player", 1f);

                pianoFlower.SetActive(false);
                isInteract_PianoFlower = true;      
                
                mirror.SetActive(false);
                Cracking_mirror.SetActive(true);

                //Debug.Log("PianoFlower Clear"); 
            }
        }
    }

    void PianoBGM_Player()
    {
        pianoBGM.Play();
    }

    void Door_Interactive()
    {
        if(hit.collider.tag == "Door" && isInteract_PianoFlower && doorHinge.canObjectCheck)
        {
            doorHinge.transform.eulerAngles = new Vector3(0, -90, 0);
        }      
    }

    void DestroyMNQ()
    {
        setMNQNewPosition.mNQ_D.SetActive(false);
       // Debug.Log("Destart");
    }

    void Soju_Interactive()
    {
        if(hit.collider.tag == "Soju")
        {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);

            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            
            haveSoju = true;
            position_Soju.SetActive(true);
            soju.SetActive(false);
        }
    }

    void FallenLeaves_Interactive()
    {
        if (hit.collider.tag == "FallenLeaves")
        {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);
            haveSoju = false;
            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            
            haveFallenLeaves = true;

            fallenLeaves.SetActive(false);
        }
    }

    void Acceptance_Mobile_Interactive()
    {
        if (hit.collider.tag == "Acceptance_Mobile")
        {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);

            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            haveAcceptance_Mobile = true;

            acceptance_Mobile.SetActive(false);
        }
    }

    void Cant_Playermove()
    {
        playerControler.enabled = true;
    }
}

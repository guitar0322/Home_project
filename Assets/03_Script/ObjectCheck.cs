using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gamesystem;

public class ObjectCheck : MonoBehaviour
{
    public float m_RayDistance = 100f;
    public bool diaryMax01 = true;

    public bool mNQMove = false;
    public bool can_Interact_Level1_Objects = false;


    public bool viewMode = false;
    public bool uiMode = false;
    public bool takeFlag = false;

    private int puzzleType;

    public GameObject mNQ_DPrefabs;
    public GameObject book_paper;
    Book book;
    public AutoFlip flip;

    //###### 상호작용 오브젝트들 초기화 칸 ###################
    public GameObject scarf;
    public GameObject fishBread;
    public GameObject medicine_Envelope_A;
    public GameObject medicine_Envelope_B;
    public GameObject medicalSchool_Pic;
    public GameObject medicalSchool_AcceptanceLetter;
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

    public AudioSource diaryDropSound;
    public AudioSource diaryOpenSound;
    public AudioSource diaryFilpSound;
    public AudioSource mirrorCrackingSound;
    public AudioSource pianoBGM;

    Player_MoveCtrl playerControler;
    ObjectControler objectControler;

    MNQPsitionCondition mNQPsition;
    OKMeshChange oKMesh;
    SetMNQNewPosition setMNQNewPosition;
    Level_2_MNQ_Position level_2_MNQ_Position;
    DoorHinge doorHinge;

    RaycastHit raycastHitObject;
    Ray ray;

    public GameObject viewModeTargetObj;
    public GameObject viewModeCamArm;
    public GameObject puzzleUI;
    public GameObject rawImage;
    public Camera viewModeCam;

    public void FlipSound()
    {
        diaryFilpSound.Play();
    }

    void Awake()
    {
        Book book_paper = GetComponent<Book>();
        playerControler = GetComponent<Player_MoveCtrl>();
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
        PressMouseRightEventHandler();
        PressKeyboardEventHandler();
    }
    void PressKeyboardEventHandler()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            TakeViewModeObject();
        }
    }
    void TakeViewModeObject()
    {
        if (viewMode == false || takeFlag == false)
            return;
        if (GameManager.instance.middleGameState == false)
            GameManager.instance.middleGameState = true;
        else
        {
            GameManager.instance.gameState++;
            GameManager.instance.middleGameState = false;
        }
        switch (raycastHitObject.collider.tag)
        {
            case "FishBread":
                //GameManager.instance.fish = true;
                book.ChangeBookPage(0);
                break;
            case "Scarf":
                //GameManager.instance.scarf = true;
                book.ChangeBookPage(2);
                break;
            case "Medicine_Envelope_A":
                book.ChangeBookPage(4);
                //GameManager.instance.medicineA = true;
                break;
            case "Medicine_Envelope_B":
                book.ChangeBookPage(6);
                //GameManager.instance.medicineB = true;
                break;
            case "MedicalSchool_Pic":
                book.ChangeBookPage(8);
                //GameManager.instance.schoolA = true;
                break;
            case "MedicalSchool_AcceptanceLetter":
                book.ChangeBookPage(10);
                //GameManager.instance.schoolB = true;
                break;

        }
        ViewModeExit();
        raycastHitObject.collider.gameObject.SetActive(false);
    }
    void PressMouseRightEventHandler()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (viewMode == true)
            {
                ViewModeExit();
                takeFlag = false;
            }
            else if (uiMode == true && flip.isFlipping == false)
            {
                UIModeExit();
            }
            Debug.Log(GameManager.instance.gameState);
        }
    }
    public void InitViewMode(GameObject target)
    {
        //렌더텍스처를 활용하지 않은 구현
        //오브젝트 상호작용 연출을 위해 오브젝트를 화면 중심으로 옮기고 오브젝트 컨트롤 스크립트를 활성화
        objectControler.SetProperty(target, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1f)));
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        playerControler.enabled = false;
        objectControler.enabled = true;
        viewMode = true;
        rawImage.SetActive(true);
    }
    public void InitUIMode(int type)
    {
        puzzleType = type;
        uiMode = true;
        playerControler.enabled = false; //유저 움직임 멈춤

        Cursor.visible = true; // 마우스 보임 
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
        puzzleUI.SetActive(true); // UI 보이기
        puzzleUI.transform.GetChild(puzzleType).gameObject.SetActive(true);
    }

    public void ViewModeExit()
    {
        playerControler.enabled = true; // 유저 다시 움직인다.
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        viewMode = false;
    }

    public void UIModeExit()
    {
        playerControler.enabled = true;
        puzzleUI.SetActive(false); //UI 숨기기
        puzzleUI.transform.GetChild(puzzleType).gameObject.SetActive(false);
        uiMode = false;
        Debug.Log("Puzzle Close");
    }

    private void ObjectCheckByRay()
    {
        if (viewMode || uiMode)
            return;
        Vector3 mouseDownPos;        
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        Debug.DrawRay(ray.origin, ray.direction * m_RayDistance, Color.red);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out raycastHitObject, m_RayDistance))
        {
            switch (raycastHitObject.collider.tag)
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
                case "Test":
                    InitViewMode(raycastHitObject.transform.gameObject);
                    break;

            }
            Debug.Log(raycastHitObject.collider.tag);
        }// 개별로 만든 이유는 나중에 각 오브젝트 별로 특별한 사운드 및 연출을 하기 위함.
    }

    void Diary_Interactive() //일기장 상호작용
    {

        Debug.Log("Diary Open");
        if (GameManager.instance.gameState >= State.O_MNQ)
        {
            if(GameManager.instance.gameState == State.O_MNQ)
                GameManager.instance.gameState++;
            InitUIMode(Puzzle.Diary);//UI모드(퍼즐모드)로 전환.

            Debug.Log("Diary Open");
        }

        //모든 오브젝트 상호작용하면
        if(GameManager.instance.gameState == State.O_SCHOOL)
        {
            Debug.Log("diary complete");// 마네킹 상호작용 가능
            mNQMove = true;
            //Debug.Log("Can MNQ Move!"); 
        }
    }

    void FirstMNQ_Interactive() //마네킹 1회 이상 상호작용 하기
    {
        if (GameManager.instance.gameState == State.START)
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
        if(GameManager.instance.gameState == State.O_DIARY)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void FishBread_Interactive() //붕어빵 상호작용 함수
    {
        if(GameManager.instance.gameState == State.O_DIARY)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void Medicine_Envelope_A_Interactive() //약 봉투A 상호작용 함수
    {
        if (GameManager.instance.gameState == State.O_FISH_SCARF)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void Medicine_Envelope_B_Interactive() //약 봉투A 상호작용 함수
    {
        if (GameManager.instance.gameState == State.O_FISH_SCARF)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void MedicalSchool_Pic_Interactive() //약 봉투A 상호작용 함수
    {
        if (GameManager.instance.gameState == State.O_MEDICINE)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void MedicalSchool_AcceptanceLetter_Interactive() //약 봉투A 상호작용 함수
    {
        if (GameManager.instance.gameState == State.O_MEDICINE)
        {
            takeFlag = true;
        }
        else
        {
            takeFlag = false;
        }
        InitViewMode(raycastHitObject.transform.gameObject);
    }

    void PianoFlower_Interactive() //피아노꽃 함수
    {
        if (GameManager.instance.gameState == State.O_MNQ_MOVE)
        {
            InitUIMode(Puzzle.Piano);
        }
    }

    void PianoBGM_Player()
    {
        pianoBGM.Play();
    }

    void Door_Interactive()
    {
            doorHinge.transform.eulerAngles = new Vector3(0, -90, 0);
    }

    void DestroyMNQ()
    {
        setMNQNewPosition.mNQ_D.SetActive(false);
       // Debug.Log("Destart");
    }

    void Soju_Interactive()
    {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);

            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            
            haveSoju = true;
            position_Soju.SetActive(true);
            soju.SetActive(false);
    }

    void FallenLeaves_Interactive()
    {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);
            haveSoju = false;
            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            
            haveFallenLeaves = true;

            fallenLeaves.SetActive(false);
    }

    void Acceptance_Mobile_Interactive()
    {
            randomTime = Random.Range(randomInt_Min_Time, randomInt_Max_Time);
            //Debug.Log(randomTime);

            setMNQNewPosition.Instantiate_MNQ();
            isCanMNQMove = true;
            haveAcceptance_Mobile = true;

            acceptance_Mobile.SetActive(false);
    }

    void Cant_Playermove()
    {
        playerControler.enabled = true;
    }
}

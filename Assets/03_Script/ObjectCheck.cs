using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Gamesystem;

public class ObjectCheck : MonoBehaviour
{
    [Header("Property")]
    public float m_RayDistance = 100f;
    public GameObject equipPoint;

    [Header ("Flags")]
    public bool mNQMove = false;
    public bool viewMode = false;
    public bool uiMode = false;
    public bool takeFlag = false;

    [Header("Interaction")]
    RaycastHit raycastHitObject;
    Ray ray;

    [Header("Object")]
    public GameObject blackHand;
    public GameObject[] TtargetPoint;

    [Header("Viewmode")]
    public GameObject viewModeTargetObj;
    public GameObject blur;
    public Camera viewModeCam;

    [Header("Puzzle")]
    private int puzzleType;
    public GameObject puzzleUI;
    public Book book;
    public AutoFlip flip;

    [Header("External Component")]
    public MNQSpawn mnqSpawner;

    [Header("Debug")]
    public Vector3 equipMNQPosition;
    //############ 일기장 이후 오브젝트 #######################
    public GameObject pianoFlower;
    public bool isInteract_PianoFlower;

    public GameObject level_1_Door01;

    public GameObject mirror;
    public GameObject Cracking_mirror;

    NavMeshAgent navMNQ;
    //#########################################################

    public AudioSource diaryDropSound;
    public AudioSource diaryOpenSound;
    public AudioSource diaryFilpSound;
    public AudioSource mirrorCrackingSound;
    public AudioSource pianoBGM;

    //component
    Player_MoveCtrl playerControler;
    ObjectControler objectControler;



    public void FlipSound()
    {
        diaryFilpSound.Play();
    }

    void Awake()
    {
        playerControler = GetComponent<Player_MoveCtrl>();
        objectControler = GetComponent<ObjectControler>();

        diaryOpenSound = GameObject.Find("DiaryOpenSound").GetComponent<AudioSource>(); // 자식 0 = OpenSound
        diaryFilpSound = GameObject.Find("DiaryFilpSound").GetComponent<AudioSource>(); // 자식 1 = FilpSound 

        mirrorCrackingSound = GameObject.Find("CrackingSound").GetComponent<AudioSource>();
        pianoBGM = GameObject.Find("PianoBGM").GetComponent<AudioSource>();


    }

    void Start()
    {
        diaryDropSound = transform.GetChild(1).GetComponent<AudioSource>(); // 자식 2 = DropSound   
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

        target.layer = LayerMask.NameToLayer("UI");
        blur.SetActive(true);
        playerControler.enabled = false;
        objectControler.enabled = true;
        viewMode = true;
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
        blur.SetActive(false);
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
                    MNQ_Interactive();//마네킹 상호작용
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
                case "Sheet":
                    Sheet_Interactive();
                    break;
                case "FallenLeaves":
                    FallenLeaves_Interactive();                     // 낙엽 함수
                    break;
                case "Acceptance_Mobile":
                    Acceptance_Mobile_Interactive();                // 모바일 합격장 함수            
                    break;
                case "Candle":
                    Candle_Interactive();
                    break;
                case "RightDoor":
                    RightDoor_Interactive();
                    break;
                case "Test":
                    InitViewMode(raycastHitObject.transform.gameObject);
                    break;

            }
            Debug.Log(raycastHitObject.collider.tag);
        }// 개별로 만든 이유는 나중에 각 오브젝트 별로 특별한 사운드 및 연출을 하기 위함.
    }
    void RightDoor_Interactive()
    {
        if (GameManager.instance.gameState == State.T_CANDLE)
        {
            raycastHitObject.transform.eulerAngles = new Vector3(-90, 0, 215);
        }
    }
    void Candle_Interactive()
    {
        if(GameManager.instance.gameState == State.O_PIANO_PUZZLE)
        {
            GameManager.instance.gameState++;
            //equip candle
        }
    }
    void Diary_Interactive() //일기장 상호작용
    {
        Debug.Log(GameManager.instance.gameState);
        if (GameManager.instance.gameState >= State.O_MNQ)
        {
            if (GameManager.instance.gameState == State.O_MNQ)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_FISH_SCARF)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_MEDICINE)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_SCHOOL)
                GameManager.instance.gameState++;
            InitUIMode(Puzzle.Diary);//UI모드(퍼즐모드)로 전환.
            Debug.Log(GameManager.instance.gameState);

            Debug.Log("Diary Open");
        }
    }

    void MNQ_Interactive()
    {
        Debug.Log(GameManager.instance.gameState);
        if (GameManager.instance.gameState == State.START)
        {
            DropDiary();
            GameManager.instance.gameState++;
            //Debug.Log("DiaryContent Can Open & Close");                
        }

        else if(GameManager.instance.gameState == State.O_DIARY_COMPLETE)
        {
            Debug.Log("test");
            raycastHitObject.transform.SetParent(equipPoint.transform);
            raycastHitObject.transform.localPosition = equipMNQPosition;
            raycastHitObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            playerControler.PickUp(raycastHitObject.transform.gameObject);
        }

    }

    private void DropDiary() // 일기장 떨어지는 함수
    {
        GameObject diary = GameObject.FindWithTag("Diary");
        diary.transform.localPosition += new Vector3(0, -1.181f, -0.965f);
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
        if (GameManager.instance.gameState == State.O_SECOND_DIARY)
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
        if (GameManager.instance.gameState == State.O_SECOND_DIARY)
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
        if (GameManager.instance.gameState == State.O_THIRD_DIARY)
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
        if (GameManager.instance.gameState == State.O_THIRD_DIARY)
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
        Debug.Log("test");
        if(GameManager.instance.gameState == State.O_PIANO_PUZZLE)
        {
            raycastHitObject.transform.eulerAngles = new Vector3(-90, 0, -145);
        }
    }

    void Soju_Interactive()
    {
        if(GameManager.instance.gameState == State.T_CANDLE)
        {
            GameManager.instance.gameState++;
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            TtargetPoint[0].SetActive(true);
            GameManager.instance.SwapLightSetting(true);
        }
        //Debug.Log(randomTime);
    }

    void Sheet_Interactive()
    {
        if(GameManager.instance.gameState == State.T_SPOT_FIRST)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            //악보 소유 UI 활성화
        }
    }
    void FallenLeaves_Interactive()
    {
    }

    void Acceptance_Mobile_Interactive()
    {
    }

    void Cant_Playermove()
    {
        playerControler.enabled = true;
    }
}

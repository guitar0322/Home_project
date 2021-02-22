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
    public GameObject fallenLeaves;
    public GameObject acceptance;
    public GameObject insence;
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
    public Vector3[] blackHandPosition;
    //############ 일기장 이후 오브젝트 #######################

    //component
    Player_MoveCtrl playerControler;
    ObjectControler objectControler;




    void Awake()
    {
        playerControler = GetComponent<Player_MoveCtrl>();
        objectControler = GetComponent<ObjectControler>();



    }

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
            ObjectCheckByRay();
        PressMouseRightEventHandler();
        PressKeyboardEventHandler();
    }
    void PressKeyboardEventHandler()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            TakeViewModeObject();
            InteractMNQ();
        }
    }
    void InteractMNQ()
    {
        if (viewMode == true)
            return;
        Vector3 mouseDownPos;
        RaycastHit raycastHitObject;
        Ray ray;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        if(Physics.Raycast(ray, out raycastHitObject, m_RayDistance))
        {
            Debug.Log(raycastHitObject.collider.tag);
            if (!raycastHitObject.collider.tag.Equals("TMNQ"))
            {
                return;
            }

            switch (GameManager.instance.gameState)
            {
                case State.T_SHEET:
                    GameManager.instance.gameState++;
                    GameManager.instance.UnActiveUI("Sheet");
                    break;
                case State.T_FLOWER:
                    GameManager.instance.gameState++;
                    GameManager.instance.UnActiveUI("Flower");
                    blackHand.SetActive(false);
                    insence.SetActive(false);
                    acceptance.SetActive(true);
                    break;
                case State.T_ACCEPTANCE:
                    break;
            }
        }
        else
        {
            Debug.Log("ray fail");
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
        if(takeFlag == true)
        {
            //show ui press f key
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
        }
    }
    public void InitViewMode(GameObject target)
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        target.layer = LayerMask.NameToLayer("UI");
        blur.SetActive(true);
        playerControler.enabled = false;
        objectControler.enabled = true;
        objectControler.SetProperty(target, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1f)));
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
        blur.SetActive(false);
        objectControler.Rollback();
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
        Vector3 mouseUpPos;        
        mouseUpPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseUpPos);
        Debug.DrawRay(ray.origin, ray.direction * m_RayDistance, Color.red);

        if (Physics.Raycast(ray, out raycastHitObject, m_RayDistance))
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
                case "PianoFlower":
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
                case "Piano":
                    Piano_Interactive();
                    break;
                case "FallenLeaves":
                    FallenLeaves_Interactive();                     // 낙엽 함수
                    break;
                case "Flower":
                    Flower_Interactive();
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
        }
        else
        {
            Debug.Log("object ray fail");
        }
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
        SoundManager.instance.diaryDropSound.Play();
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
            blackHand.transform.position = blackHandPosition[0];
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
            GameManager.instance.ActiveUI("Sheet");
            raycastHitObject.transform.gameObject.SetActive(false);
        }
    }

    void Piano_Interactive()
    {
        if(GameManager.instance.gameState == State.T_MNQ_FIRST)
        {
            GameManager.instance.gameState++;
            fallenLeaves.SetActive(true);
            blackHand.SetActive(false);
        }
    }

    void FallenLeaves_Interactive()
    {
        if (GameManager.instance.gameState == State.T_PIANO)
        {
            GameManager.instance.gameState++;
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            blackHand.transform.position = blackHandPosition[1];
            TtargetPoint[1].SetActive(true);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    void Flower_Interactive()
    {
        if (GameManager.instance.gameState == State.T_SPOT_SECOND)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            GameManager.instance.ActiveUI("Flower");
            raycastHitObject.transform.gameObject.SetActive(false);
            //카메라 물이 일렁이는 듯한 화면 효과
        }
    }

    void Acceptance_Mobile_Interactive()
    {
        if (GameManager.instance.gameState == State.T_MNQ_SECOND)
        {
            GameManager.instance.gameState++;
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            blackHand.transform.position = blackHandPosition[2];
            TtargetPoint[2].SetActive(true);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    void Cant_Playermove()
    {
        playerControler.enabled = true;
    }
}

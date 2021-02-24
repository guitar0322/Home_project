using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class ObjectManager : MonoBehaviour
{
    public ViewMode viewMode;
    RaycastHit raycastHitObject;
    ObjectInfo targetInfo;
    PlayerControler playerControler;

    [Header("Level1 Object")]
    public GameObject diary;

    [Header("Level2 Object")]
    public GameObject blackHand;
    public GameObject fallenLeaves;
    public GameObject insence;
    public GameObject acceptance;
    public GameObject[] TtargetPoint;

    [Header ("Component")]
    public MNQSpawner mnqSpawner;
    public EyeSpawner eyeSpawner;
    public void InteractionObject(RaycastHit target)
    {
        raycastHitObject = target;
        targetInfo = target.transform.GetComponent<ObjectInfo>();
        if (targetInfo != null && targetInfo.viewModeFlag)
        {
            InteractionViewModeObject();
        }
        switch (raycastHitObject.collider.tag)
        {
            case "MNQ":
                MNQ_Interactive();//마네킹 상호작용
                break;
            case "Diary":
                Diary_Interactive();//일기장 함수
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
                viewMode.InitViewMode(target.transform.gameObject, false);
                break;

        }
        Debug.Log(target.collider.tag);
    }

    public void InteractMNQ()
    {
        Vector3 mouseDownPos;
        RaycastHit raycastHitObject;
        Ray ray;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        if (Physics.Raycast(ray, out raycastHitObject, playerControler.m_RayDistance))
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
                    GameManager.instance.UnActiveItemUI("Sheet");
                    break;
                case State.T_FLOWER:
                    GameManager.instance.gameState++;
                    GameManager.instance.UnActiveItemUI("Flower");
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

    void InteractionViewModeObject()
    {
        if(targetInfo.gameStateCondition == GameManager.instance.gameState)
        {
            viewMode.InitViewMode(raycastHitObject.transform.gameObject, true);
        }
        else
        {
            viewMode.InitViewMode(raycastHitObject.transform.gameObject, false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControler = GameManager.instance.playerControler;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RightDoor_Interactive()
    {
        if (GameManager.instance.gameState == State.T_CANDLE)
        {
            GameManager.instance.gameState++;
            raycastHitObject.transform.localEulerAngles = new Vector3(-90, 225, 45);
        }
    }

    void Candle_Interactive()
    {
        if (GameManager.instance.gameState == State.O_DOOR)
        {
            GameManager.instance.gameState++;
        }
        playerControler.EquipItem(raycastHitObject.transform.gameObject, GameManager.instance.candleEquipPostion);
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
            GameManager.instance.InitUIMode(Puzzle.Diary);//UI모드(퍼즐모드)로 전환.
        }
    }

    void MNQ_Interactive()
    {
        Debug.Log(GameManager.instance.gameState);
        if (GameManager.instance.gameState == State.START)
        {
            diary.transform.position = GameManager.instance.diaryDropPosition.position;
            diary.transform.Rotate(0, 0, 90);
            SoundManager.instance.diaryDropSound.Play();
            GameManager.instance.gameState++;
            //Debug.Log("DiaryContent Can Open & Close");                
        }

        else if (GameManager.instance.gameState == State.O_DIARY_COMPLETE)
        {
            Debug.Log("test");
            playerControler.EquipItem(raycastHitObject.transform.gameObject, GameManager.instance.mnqEquipPostion);
        }
    }

    void PianoFlower_Interactive() //피아노꽃 함수
    {
        if (GameManager.instance.gameState == State.O_MNQ_MOVE)
        {
            GameManager.instance.InitUIMode(Puzzle.Piano);
        }
    }

    void Door_Interactive()
    {
        if (GameManager.instance.gameState == State.O_PIANO_PUZZLE)
        {
            GameManager.instance.gameState++;
            raycastHitObject.transform.eulerAngles = new Vector3(-90, 0, -180);
        }
    }

    void Soju_Interactive()
    {
        if (GameManager.instance.gameState == State.T_DOOR)
        {
            GameManager.instance.gameState++;
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[0].position;
            TtargetPoint[0].SetActive(true);
            GameManager.instance.SwapLightSetting(true);
            SoundManager.instance.pianoBGM.Stop();
        }
        //Debug.Log(randomTime);
    }

    void Sheet_Interactive()
    {
        if (GameManager.instance.gameState == State.T_SPOT_FIRST)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            GameManager.instance.ActiveItemUI("Sheet");
            raycastHitObject.transform.gameObject.SetActive(false);
        }
    }

    void Piano_Interactive()
    {
        if (GameManager.instance.gameState == State.T_MNQ_FIRST)
        {
            GameManager.instance.gameState++;
            fallenLeaves.SetActive(true);
            blackHand.SetActive(false);
            mnqSpawner.DisableMNQ(0);
            TtargetPoint[0].SetActive(false);
        }
    }

    void FallenLeaves_Interactive()
    {
        if (GameManager.instance.gameState == State.T_PIANO)
        {
            GameManager.instance.gameState++;
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[1].position;
            TtargetPoint[1].SetActive(true);
            raycastHitObject.transform.gameObject.SetActive(false);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    void Flower_Interactive()
    {
        if (GameManager.instance.gameState == State.T_SPOT_SECOND)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            GameManager.instance.ActiveItemUI("Flower");
            raycastHitObject.transform.gameObject.SetActive(false);
            //카메라 물이 일렁이는 듯한 화면 효과
        }
    }

    void Acceptance_Mobile_Interactive()
    {
        if (GameManager.instance.gameState == State.T_MNQ_SECOND)
        {
            GameManager.instance.gameState++;
            mnqSpawner.DisableMNQ(0);
            mnqSpawner.SpawnMNQ(1);
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[2].position;
            TtargetPoint[2].SetActive(true);
            TtargetPoint[1].SetActive(false);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    public void SpawnMNQ(int num)
    {
        mnqSpawner.SpawnMNQ(num);
    }

    public void StopMNQ()
    {
        mnqSpawner.StopMNQ();
    }

    public void SpawnEye()
    {
        eyeSpawner.SpawnEye();
    }
}

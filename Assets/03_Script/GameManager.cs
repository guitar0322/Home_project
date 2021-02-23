using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [Header ("Other")]
    public int gameState;
    public bool middleGameState;
    public GameObject leftDoor;
    public float slowWeight = 1;
    public PlayerControler playerControler;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }

    public void StageInit()
    {
        //gameState = 0;
        middleGameState = false;
    }
    IEnumerator DisableControler(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerControler.enabled = true;
    }
    public void DisableControlerInSec(int seconds)
    {
        playerControler.enabled = false;
        StartCoroutine("DisableControler", seconds);
    }

    public void StageOneClear()
    {
        leftDoor.transform.eulerAngles = new Vector3(0, -10, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        StageInit(); // 이후에 시작하기 버튼 혹은 재도전 등의 게임시작 트리거위치에서 호출되도록 수정
    }
}

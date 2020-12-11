using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int gameState;
    Player_MoveCtrl player_MoveCtrl;
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
        player_MoveCtrl = GameObject.FindWithTag("Player").GetComponent<Player_MoveCtrl>();
        gameState = 0;
    }

    public Player_MoveCtrl GetPlayerControler()
    {
        return player_MoveCtrl;
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

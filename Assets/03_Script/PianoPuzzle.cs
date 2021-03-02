using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class PianoPuzzle : MonoBehaviour
{
    public ObjectManager objectManager;
    public GameObject piecePosSet;
    public GameObject pieceSet;
    
    // Start is called before the first frame update

    public bool IsClear()
    {
        for (int i = 0; i < piecePosSet.transform.childCount; i++)
        {
            if(piecePosSet.transform.GetChild(i).childCount == 0)
            {
                return false;
            }
            else if(i != piecePosSet.transform.GetChild(i).GetChild(0).GetComponent<PuzzlePiece>().puzzle_no)
            {
                return false;
            }
        }

        GameManager.instance.gameState++;
        GameManager.instance.ExitUIMode();
        SoundManager.instance.mirrorCrackingSound.Play();
        SoundManager.instance.pianoBGM.Play();

        objectManager.pianoFlower.SetActive(false);

        objectManager.mirror.SetActive(false);
        objectManager.crackingMirror.SetActive(true);
        objectManager.OpenLeftDoor();
        
        return true;
    }
    void Start()
    {
        InitPiecePos();
    }

    void InitPiecePos()
    {
        int ranX, ranY;
        for(int i = 0; i < piecePosSet.transform.childCount; i++)
        {
            ranX = Random.Range(300, 1500);
            ranY = Random.Range(100, 900);
            pieceSet.transform.GetChild(i).transform.position = new Vector3(ranX, ranY, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

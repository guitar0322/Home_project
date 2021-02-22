using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;

public class PianoPuzzle : MonoBehaviour
{
    public GameObject piecePosSet;
    public GameObject pieceSet;
    public GameObject pianoFlower;
    public GameObject mirror;
    public GameObject Cracking_mirror;
    public ObjectCheck objectCheck;
    
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

        GameManager.instance.gameState = State.O_PIANO_PUZZLE;
        objectCheck.UIModeExit();
        SoundManager.instance.mirrorCrackingSound.Play();
        SoundManager.instance.pianoBGM.Play();

        pianoFlower.SetActive(false);

        mirror.SetActive(false);
        Cracking_mirror.SetActive(true);
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

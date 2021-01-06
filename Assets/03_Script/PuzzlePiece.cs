using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private int clampOffset;
    public PianoPuzzle pianoPuzzle;
    public int puzzle_no;
    public ObjectCheck objectCheck;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(transform.position);
    }

    bool CheckPuzzleSocketSnap()
    {
        for (int i = 0; i < pianoPuzzle.piecePosSet.transform.childCount; i++)
        {
            //Debug.Log("pos : " + pianoPuzzle.piecePos[i].transform.position);
            if (Mathf.Abs(transform.position.x - pianoPuzzle.piecePosSet.transform.GetChild(i).position.x) <= clampOffset &&
                Mathf.Abs(transform.position.y - pianoPuzzle.piecePosSet.transform.GetChild(i).position.y) <= clampOffset)
            {
                if (pianoPuzzle.piecePosSet.transform.GetChild(i).childCount == 0)
                {
                    transform.SetParent(pianoPuzzle.piecePosSet.transform.GetChild(i).transform);
                    transform.localPosition = Vector3.zero;
                    return true;
                }
            }
        }
        return false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CheckPuzzleSocketSnap())
        {
            transform.SetParent(pianoPuzzle.pieceSet.transform);
        }


        if (pianoPuzzle.IsClear())
        {
            GameManager.instance.StageOneClear();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzle_no = this.name[this.name.Length - 1] - '0';
        clampOffset = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamesystem;
public partial class GameManager
{
    [Header ("ItemUI")]
    public GameObject ItemUISet;
    public Sprite sheet;
    public Sprite flower;
    public Sprite acceptance;

    [Header("PuzzleUI")]
    public GameObject puzzleUI;
    public AutoFlip diaryFlip;
    public Book diaryUI;
    private int activatedPuzzle;

    [Header("Flag")]
    private bool uiMode;

    public void InitUIMode(int type)
    {
        if (uiMode == true)
            return;
        uiMode = true;
        playerControler.controlFlag = false; //유저 움직임 멈춤

        Cursor.visible = true; // 마우스 보임 
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
        puzzleUI.SetActive(true); // UI 보이기
        puzzleUI.transform.GetChild(type).gameObject.SetActive(true);
        activatedPuzzle = type;
    }

    public void ExitUIMode()
    {
        if (uiMode == false)
            return;
        if (activatedPuzzle == Puzzle.Diary && diaryFlip.isFlipping == true)
            return;
        puzzleUI.SetActive(false); //UI 숨기기
        puzzleUI.transform.GetChild(activatedPuzzle).gameObject.SetActive(false);
        playerControler.controlFlag = true;
        uiMode = false;
        Debug.Log("Puzzle Close");
    }
    public void ActiveItemUI(string tag)
    {
        GameObject itemUI;
        for(int i = 0; i < ItemUISet.transform.childCount; i++)
        {
            itemUI = ItemUISet.transform.GetChild(i).gameObject;
            if (itemUI.activeSelf == false)
            {
                itemUI.GetComponent<ItemUI>().SetProperty(tag);
                itemUI.SetActive(true);
            }
        }
    }
    public void UnActiveItemUI(string tag)
    {
        GameObject itemUI;
        for(int i = 0; i < ItemUISet.transform.childCount; i++)
        {
            itemUI = ItemUISet.transform.GetChild(i).gameObject;
            if (itemUI.GetComponent<ItemUI>().tag.Equals(tag))
            {
                itemUI.SetActive(false);
            }
        }
    }
}

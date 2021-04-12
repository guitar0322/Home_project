using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    class Data
    {
        public Vector3 playerPosition;
        public int gameState;
        public bool[] O_ObjectActive;
        public Vector3[] O_ObjectPosition;
        public Vector3[] O_ObjectAngle;
        public bool[] T_ObjectActive;
        public Vector3[] T_ObjectPosition;
        public Vector3[] T_ObjectAngle;
        public bool[] TH_ObjectActive;
        public Vector3[] TH_ObjectPosition;
        public Vector3[] TH_ObjectAngle;
    }

    [Header("SaveManager")]
    private Data saveData;
    public Transform O_Object;
    public Transform T_Object;
    public Transform TH_Object;
    public Book book;

    public void SaveData()
    {
        saveData = new Data();
        saveData.playerPosition = playerControler.transform.position;
        saveData.gameState = gameState;

        saveData.O_ObjectActive = new bool[O_Object.childCount];
        saveData.O_ObjectPosition = new Vector3[O_Object.childCount];
        saveData.O_ObjectAngle = new Vector3[O_Object.childCount];
        for (int i = 0; i < O_Object.childCount; i++)
        {
            saveData.O_ObjectActive[i] = O_Object.GetChild(i).gameObject.activeSelf;
            saveData.O_ObjectPosition[i] = O_Object.GetChild(i).transform.position;
            saveData.O_ObjectAngle[i] = O_Object.GetChild(i).transform.eulerAngles;
        }
        saveData.T_ObjectActive = new bool[T_Object.childCount];
        saveData.T_ObjectPosition = new Vector3[T_Object.childCount];
        saveData.T_ObjectAngle = new Vector3[T_Object.childCount];
        for (int i = 0; i < T_Object.childCount; i++)
        {
            saveData.T_ObjectActive[i] = T_Object.GetChild(i).gameObject.activeSelf;
            saveData.T_ObjectPosition[i] = T_Object.GetChild(i).transform.position;
            saveData.T_ObjectAngle[i] = T_Object.GetChild(i).transform.eulerAngles;
        }

        saveData.TH_ObjectActive = new bool[TH_Object.childCount];
        saveData.TH_ObjectPosition = new Vector3[TH_Object.childCount];
        saveData.TH_ObjectAngle = new Vector3[TH_Object.childCount];
        for (int i = 0; i < TH_Object.childCount; i++)
        {
            saveData.TH_ObjectActive[i] = TH_Object.GetChild(i).gameObject.activeSelf;
            saveData.TH_ObjectPosition[i] = TH_Object.GetChild(i).transform.position;
            saveData.TH_ObjectAngle[i] = TH_Object.GetChild(i).transform.eulerAngles;
        }
    }

    public void LoadData()
    {
        playerControler.transform.position = saveData.playerPosition;
        gameState = saveData.gameState;
        middleGameState = false;
        book.InitPage();

        for(int i = 0; i < O_Object.childCount; i++)
        {
            O_Object.GetChild(i).gameObject.SetActive(saveData.O_ObjectActive[i]);
            O_Object.GetChild(i).transform.position = saveData.O_ObjectPosition[i];
            O_Object.GetChild(i).transform.eulerAngles = saveData.O_ObjectAngle[i];
        }

        for (int i = 0; i < T_Object.childCount; i++)
        {
            T_Object.GetChild(i).gameObject.SetActive(saveData.T_ObjectActive[i]);
            T_Object.GetChild(i).transform.position = saveData.T_ObjectPosition[i];
            T_Object.GetChild(i).transform.eulerAngles = saveData.T_ObjectAngle[i];
        }

        for (int i = 0; i < TH_Object.childCount; i++)
        {
            TH_Object.GetChild(i).gameObject.SetActive(saveData.TH_ObjectActive[i]);
            TH_Object.GetChild(i).transform.position = saveData.TH_ObjectPosition[i];
            TH_Object.GetChild(i).transform.eulerAngles = saveData.TH_ObjectAngle[i];
        }
    }
}

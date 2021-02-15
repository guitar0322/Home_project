using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetProperty(string tag)
    {
        if(GameManager.instance.gameState == State.T_SHEET)
        {
            GetComponent<Image>().sprite = GameManager.instance.sheet;
        }
        else if (GameManager.instance.gameState == State.T_FLOWER)
        {
            GetComponent<Image>().sprite = GameManager.instance.flower;
        }
        this.tag = tag;
    }
}

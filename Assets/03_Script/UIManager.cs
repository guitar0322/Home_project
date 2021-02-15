using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager
{
    [Header ("UIManager")]
    public GameObject ItemUISet;
    public Sprite sheet;
    public Sprite flower;
    public Sprite acceptance;

    public void ActiveUI(string tag)
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
    public void UnActiveUI(string tag)
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

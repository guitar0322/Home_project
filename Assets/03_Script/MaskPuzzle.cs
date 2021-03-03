using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPuzzle : MonoBehaviour
{
    public THTrigger thTrigger;
    public ObjectManager objectManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IsClear()
    {
        GameManager.instance.ExitUIMode();
        GameManager.instance.gameState++;
        objectManager.secondTrapWall.SetActive(false);
        thTrigger.Next();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

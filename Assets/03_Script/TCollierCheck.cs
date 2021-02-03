using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;
public class TCollierCheck : MonoBehaviour
{
    public GameObject sheet;
    public GameObject fallenLeaf;
    public GameObject acceptance;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.gameState == State.T_SOJU && other.tag.Equals("TMNQ"))
        {
            GameManager.instance.SwapLightSetting(false);
            sheet.SetActive(true);
            GameManager.instance.gameState++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;
public class TCollierCheck : MonoBehaviour
{
    public GameObject sheet;
    public GameObject flower;
    public GameObject lasso;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " , " + GameManager.instance.gameState);
        if (other.tag.Equals("TMNQ"))
        {
            if (GameManager.instance.gameState == State.T_SOJU)
            {
                sheet.SetActive(true);
            }
            else if (GameManager.instance.gameState == State.T_LEAVES)
            {
                flower.SetActive(true);
            }
            else if(GameManager.instance.gameState == State.T_ACCEPTANCE)
            {
                lasso.SetActive(true);
            }
            GameManager.instance.gameState++;
            GameManager.instance.SwapLightSetting(false);
            other.isTrigger = false;
            other.GetComponent<NavMNQ>().StopFollow();
            other.GetComponent<NavMNQ>().enabled = false;
            this.enabled = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

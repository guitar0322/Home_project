using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;
public class TCollierCheck : MonoBehaviour
{
    public GameObject sheet;
    public GameObject flower;
    public GameObject lasso;

    public MNQSpawn mnqSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TMNQ") && GameManager.instance.gameState < State.T_MNQ_THIRD)
        {
            other.isTrigger = false;
            other.GetComponent<NavMNQ>().StopFollow();
            other.GetComponent<NavMNQ>().enabled = false;
            this.enabled = false;
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
                GameManager.instance.DisableControlerInSec(GameManager.instance.disableControlSec);
                mnqSpawner.SpawnMNQ(GameManager.instance.TMNQSpawnNum);
                GameManager.instance.slowWeight = GameManager.instance.slowScale;
            }
            GameManager.instance.gameState++;
            GameManager.instance.SwapLightSetting(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

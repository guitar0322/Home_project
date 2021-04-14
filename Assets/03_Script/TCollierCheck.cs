using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamesystem;
public class TCollierCheck : MonoBehaviour
{
    public GameObject sheet;
    public GameObject flower;
    public GameObject lasso;

    public ObjectManager objectManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TMNQ") && GameManager.instance.gameState < State.T_SPOT_THIRD)
        {
            other.isTrigger = false;
            other.GetComponent<NavMNQ>().StopFollow();
            this.enabled = false;
            objectManager.traceMNQSpawner.isSpotMNQ = true;
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
                GameManager.instance.playerControler.moveControlFlag = false;
                GameManager.instance.slowWeight = GameManager.instance.slowScale;
                objectManager.traceMNQSpawner.enabled = false;
                StartCoroutine("SpawnLassoMNQ");
            }
            if (GameManager.instance.gameState != State.T_ACCEPTANCE)
                GameManager.instance.gameState++;
            GameManager.instance.SwapLightSetting(false);
        }
    }
    IEnumerator SpawnLassoMNQ()
    {
        yield return new WaitForSeconds(GameManager.instance.disableControlSec);
        objectManager.SpawnLassoMNQ(GameManager.instance.TMNQSpawnNum);
        objectManager.lassoMNQSpawner.InitSpawn();
        StartCoroutine("SpawnEye");
    }
    IEnumerator SpawnEye()
    {
        yield return new WaitForSeconds(GameManager.instance.waitSpawnEyeTime);
        objectManager.SpawnEye();
        yield return new WaitForSeconds(GameManager.instance.waitSpawnEyeTime + GameManager.instance.eyeScalingTime + GameManager.instance.waitLookPlayerTime);
        objectManager.StopLassoMNQ();
        GameManager.instance.slowWeight = 1;
        GameManager.instance.gameState++;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

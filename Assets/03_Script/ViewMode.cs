using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMode : MonoBehaviour
{
    public bool viewMode;
    private GameObject viewModeTargetObj;
    private ObjectInfo targetInfo;
    public GameObject blur;
    public Camera viewModeCam;
    public bool takeFlag;

    [Header("Component")]
    public ObjectControler objectControler;
    PlayerControler playerControler;

    // Start is called before the first frame update
    void Start()
    {
        playerControler = GameManager.instance.playerControler;
    }
    public bool InitViewMode(GameObject target, bool flag)
    {
        if (viewMode == true)
            return false;
        viewModeTargetObj = target;
        targetInfo = viewModeTargetObj.GetComponent<ObjectInfo>();
        takeFlag = flag;
        if(takeFlag == true)
        {
            GameManager.instance.fKeyUI.SetActive(true);
        }
        viewMode = true;

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        target.layer = LayerMask.NameToLayer("UI");

        blur.SetActive(true);
        objectControler.enabled = true;
        objectControler.SetProperty(target, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1f)));
        playerControler.moveControlFlag = false;
        playerControler.rotateControlFlag = false;
        return true;
    }

    public void TakeViewModeObject()
    {
        if (viewMode == false || takeFlag == false)
            return;
        if (GameManager.instance.middleGameState == false)
            GameManager.instance.middleGameState = true;
        else
        {
            GameManager.instance.gameState++;
            GameManager.instance.middleGameState = false;
        }
        if (takeFlag == true)
        {
            //show ui press f key
        }
        int changePage = (targetInfo.gameStateCondition / 2 - 1) * 4 + (targetInfo.diarySecondFlag?2:0);
        GameManager.instance.diaryUI.ChangeBookPage(changePage);
        ViewModeExit();
        viewModeTargetObj.SetActive(false);
    }

    public void ViewModeExit()
    {
        if (viewMode == false)
            return;

        playerControler.moveControlFlag = true;
        playerControler.rotateControlFlag = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        blur.SetActive(false);
        objectControler.Rollback();
        GameManager.instance.fKeyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

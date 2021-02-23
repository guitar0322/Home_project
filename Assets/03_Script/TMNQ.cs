using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMNQ : MonoBehaviour
{
    private int minTime;
    private int maxTime;
    private float _WaitTime;
    private float waitTime;
    NavMNQ navMNQ;
    private void OnEnable()
    {
        _WaitTime = Random.Range(minTime, maxTime);
        waitTime = 0;
        if(navMNQ != null)
            navMNQ.enabled = true;
    }
    public void SetProperty(int waitMinTime, int waitMaxTime, bool flag)
    {
        minTime = waitMinTime;
        maxTime = waitMaxTime;
        if(flag == true)
        {
            navMNQ.SetSpeed();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        navMNQ = GetComponent<NavMNQ>();
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= _WaitTime)
        {
            navMNQ.followFlag = true;
        }
    }
}

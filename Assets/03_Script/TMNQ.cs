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
    }
    public void SetProperty(int waitMinTime, int waitMaxTime)
    {
        minTime = waitMinTime;
        maxTime = waitMaxTime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMNQ : MonoBehaviour
{
    private float minTime;
    private float maxTime;
    private float _WaitTime;
    public NavMNQ navMNQ;
    public int posIdx;
    public void SetProperty(float waitMinTime, float waitMaxTime, bool flag, int _posIdx)
    {
        if (navMNQ != null)
            navMNQ.enabled = true;
        posIdx = _posIdx;
        minTime = waitMinTime;
        maxTime = waitMaxTime;
        _WaitTime = Random.Range(minTime, maxTime);
        if(flag == true)
        {
            navMNQ.SetSpeed();
        }
        StartCoroutine("FollowPlayer");
    }
    IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(_WaitTime);
        navMNQ.followFlag = true;
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

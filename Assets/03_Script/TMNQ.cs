using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMNQ : MonoBehaviour
{
    private float _WaitTime;
    private float waitTime;
    NavMNQ navMNQ;
    private void OnEnable()
    {
        _WaitTime = Random.Range(1, 5);
        waitTime = 0;
    }
    public void SetProperty()
    {
        
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

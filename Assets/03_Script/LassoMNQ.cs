using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LassoMNQ : MonoBehaviour
{
    private float minTime;
    private float maxTime;
    private float _WaitTime;
    public int posIdx;
    public NavMeshAgent agent;
    public Transform playerTrasnform;
    
    public void SetProperty(float waitMinTime, float waitMaxTime, bool flag, int _posIdx)
    {
        posIdx = _posIdx;
    }
    IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(_WaitTime);
        agent.SetDestination(playerTrasnform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        _WaitTime = Random.Range(GameManager.instance.TMNQWaitMinTime, GameManager.instance.TMNQWaitMaxTime);
        agent.speed = Random.Range(GameManager.instance.TMNQMinSpeed, GameManager.instance.TMNQMaxSpeed);
        StartCoroutine("FollowPlayer");
    }

    // Update is called once per frame
    void Update()
    {
    }
}

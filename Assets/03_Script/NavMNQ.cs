using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMNQ : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;
    public bool followFlag;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetSpeed()
    {
        agent.speed = Random.Range(GameManager.instance.TMNQMinSpeed, GameManager.instance.TMNQMaxSpeed) * GameManager.instance.slowWeight;
    }
    public void StopFollow()
    {
        agent.isStopped = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(followFlag == true)
            agent.SetDestination(playerTransform.position);
    }
}
